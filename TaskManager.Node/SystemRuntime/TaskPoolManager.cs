using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using NLog;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using TaskCore;
using TaskManager.ApiSdk.Factory;
using TaskManager.ApiSdk.Sdk;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Node.Helper;
using TaskManager.Utils;

namespace TaskManager.Node.SystemRuntime
{
    public class TaskPoolManager : IDisposable
    {
        private readonly Logger _logger;

        private string _sdkHost;
        private string _nodeId;
        private string _rootPath;
        private ITmSdk _sdk;
        private readonly ConcurrentDictionary<string, TaskRuntimeInfo> _taskInfos;
        private readonly IScheduler _sched;

        private static readonly object LockObj = new object();
        private static TaskPoolManager _instance;
        public static TaskPoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TaskPoolManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private TaskPoolManager()
        {
            this._logger = LogManager.GetLogger("TaskLogger");

            this._taskInfos = new ConcurrentDictionary<string, TaskRuntimeInfo>();

            ISchedulerFactory sf = new StdSchedulerFactory();
            _sched = sf.GetScheduler();
            _sched.Start();
        }

        public void Dispose()
        {
            if (_sched != null && !_sched.IsShutdown)
                _sched.Shutdown();
        }

        public void Init(string host, string nodeId, string rootPath)
        {
            lock (LockObj)
            {
                this._sdkHost = _sdkHost;
                this._nodeId = nodeId;
                this._rootPath = rootPath;
                this._sdk = SdkFactory.CreateSdk(new SdkConfig(host));

                if (this._taskInfos.Any())
                {
                    foreach (var taskRuntimeInfo in this._taskInfos)
                    {
                        AppDomain.Unload(taskRuntimeInfo.Value.AppDomain);
                    }
                    this._taskInfos.Clear();
                }

                this.InitLoadTasks();
            }
        }

        internal TaskRuntimeInfo GetByTaskId(string taskId)
        {
            return _taskInfos[taskId];
        }

        #region Private Method

        private bool InitLoadTasks()
        {
            var loadTaskResult = this._sdk.GetTasks(this._nodeId);
            if (loadTaskResult.HasError)
            {
                this._logger.Error("get task api error:{0}", loadTaskResult.ErrorMessage);
                return false;
            }

            foreach (var task in loadTaskResult.Data)
            {
                try
                {
                    if (LoadTask(task))
                    {
                        IJobDetail job = new JobDetailImpl(task.Id, null, typeof(TaskJob));
                        ITrigger trigger = new CronTriggerImpl(task.Id, null, task.Cron);
                        _sched.ScheduleJob(job, trigger);
                    }
                }
                catch (Exception ex)
                {
                    this._logger.Error("load task exception:{0}", ex.Message);
                }
            }

            return true;
        }

        private bool LoadTask(Task task)
        {
            this._logger.Trace("init task:{0}", task.Id);
            if (!this._taskInfos.ContainsKey(task.Id))
            {
                var taskDllFilePath = PrepareTaskFile(task);
                AppDomain domain = null;
                var ta = new AppDomainLoader<BaseTask>().Load(taskDllFilePath, task.ClassName, out domain);

                if (!this._taskInfos.TryAdd(task.Id, new TaskRuntimeInfo()
                {
                    AppDomain = domain,
                    TaskInfo = task,
                    ExeTask = ta,
                    TmSdk = this._sdk
                }))
                {
                    AppDomain.Unload(domain);
                    this._logger.Error("add task fail, taskId:{0}", task.Id);
                    return false;
                }
            }

            return true;
        }

        private string PrepareTaskFile(Task task)
        {
            var taskPackageFilePath = ServiceFileHelper.GetTaskPackageFilePath(this._rootPath, task.Id);
            this._logger.Trace("task file path:{0}", taskPackageFilePath);
            if (!File.Exists(taskPackageFilePath))
            {
                var downloadResult = this._sdk.DownloadTaskFile(task.Id);
                if (downloadResult.HasError)
                {
                    this._logger.Error("download task file fail, taskId:{0}", task.Id);
                    TaskRuntimeInfo t = null;
                    this._taskInfos.TryRemove(task.Id, out t);
                }
                else
                {
                    downloadResult.Data.Seek(0, SeekOrigin.Begin);
                    this._logger.Trace(downloadResult.Data.Length);
                    DirectoryAndFileHelper.SaveFile(downloadResult.Data, taskPackageFilePath);
                }
            }
            var taskFileUnzipFolderPath = ServiceFileHelper.GetTaskFileUnzipFolderPath(this._rootPath,
                    task.Id);
            if (!Directory.Exists(taskFileUnzipFolderPath))
            {
                Directory.CreateDirectory(taskFileUnzipFolderPath);
                CompressHelper.UnZip(taskPackageFilePath, taskFileUnzipFolderPath);
            }

            return string.Format("{0}{1}", taskFileUnzipFolderPath, task.DllName);
        }

        #endregion
    }
}
