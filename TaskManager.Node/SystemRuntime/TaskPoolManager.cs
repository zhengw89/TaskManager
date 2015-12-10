using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

        private static readonly object LoadTaskLockObj = new object();
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
                this._sdkHost = host;
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

                var th = new Thread(new ThreadStart(LoadTaskThreadMethod));
                th.Start();
            }
        }

        private void LoadTaskThreadMethod()
        {
            while (true)
            {
                lock (LoadTaskLockObj)
                {
                    var loadTaskResult = this._sdk.GetTasks(this._nodeId);
                    if (loadTaskResult.HasError)
                    {
                        this._logger.Error("get task api error:{0}", loadTaskResult.ErrorMessage);
                        return;
                    }

                    //unload task
                    var unloadTaskIds = new List<string>();
                    foreach (var taskInfo in this._taskInfos)
                    {
                        if (!loadTaskResult.Data.Any(a => a.Id.Equals(taskInfo.Key)))
                        {
                            unloadTaskIds.Add(taskInfo.Key);
                        }
                    }
                    foreach (var unloadTaskId in unloadTaskIds)
                    {
                        var task = this._taskInfos[unloadTaskId];

                        _sched.PauseTrigger(task.Trigger.Key);
                        _sched.UnscheduleJob(task.Trigger.Key);
                        _sched.DeleteJob(task.JobDetail.Key);

                        TaskRuntimeInfo info = null;
                        if (!this._taskInfos.TryRemove(unloadTaskId, out info))
                        {
                            this._logger.Error("Unload task fail, taskId:{0}", unloadTaskId);
                        }
                        else
                        {
                            try
                            {
                                AppDomain.Unload(info.AppDomain);

                                var taskFileUnzipFolderPath =
                                        ServiceFileHelper.GetTaskFileUnzipFolderPath(this._rootPath, unloadTaskId);
                                DirectoryAndFileHelper.DeleteFolder(taskFileUnzipFolderPath);
                            }
                            catch (Exception ex)
                            {
                                this._logger.Error("卸载任务 {0} 异常:{1}", task.TaskInfo.Name, ex.Message);
                            }
                        }
                    }

                    //load task
                    foreach (var task in loadTaskResult.Data)
                    {
                        if (!this._taskInfos.ContainsKey(task.Id))
                        {
                            var taskDllFilePath = PrepareTaskFile(task);
                            if (string.IsNullOrEmpty(taskDllFilePath))
                            {
                                this._logger.Error("PrepareTaskFile fail taskId:{0}", task.Id);
                                continue;
                            }

                            AppDomain domain = null;
                            var ta = new AppDomainLoader<BaseTask>().Load(taskDllFilePath, task.ClassName, out domain);

                            IJobDetail job = new JobDetailImpl(task.Id, null, typeof(TaskJob));
                            ITrigger trigger = new CronTriggerImpl(task.Id, null, task.Cron);
                            _sched.ScheduleJob(job, trigger);

                            if (!this._taskInfos.TryAdd(task.Id, new TaskRuntimeInfo()
                            {
                                AppDomain = domain,
                                TaskInfo = task,
                                ExeTask = ta,
                                TmSdk = SdkFactory.CreateSdk(new SdkConfig(this._sdkHost)),
                                JobDetail = job,
                                Trigger = trigger,
                                Lock = new TaskLock()
                            }))
                            {
                                AppDomain.Unload(domain);

                                this._sched.PauseTrigger(trigger.Key);
                                this._sched.UnscheduleJob(trigger.Key);
                                this._sched.DeleteJob(job.Key);

                                var taskFileUnzipFolderPath =
                                    ServiceFileHelper.GetTaskFileUnzipFolderPath(this._rootPath, task.Id);
                                DirectoryAndFileHelper.DeleteFolder(taskFileUnzipFolderPath);

                                this._logger.Error("add task fail, taskId:{0}", task.Id);
                            }
                        }
                    }
                }

                Thread.Sleep(1 * 60 * 1000);
            }
        }

        internal TaskRuntimeInfo GetByTaskId(string taskId)
        {
            return _taskInfos[taskId];
        }

        #region Private Method

        private string PrepareTaskFile(Task task)
        {
            var taskPackageFilePath = ServiceFileHelper.GetTaskPackageFilePath(this._rootPath, task.Id);

            if (!File.Exists(taskPackageFilePath))
            {
                if (!this.DownloadTaskFile(task.Id, taskPackageFilePath))
                {
                    TaskRuntimeInfo t = null;
                    this._taskInfos.TryRemove(task.Id, out t);
                    return null;
                }
            }
            else
            {
                var sb = new StringBuilder();
                using (var md5 = MD5.Create())
                {
                    using (var fileStream = new FileStream(taskPackageFilePath, FileMode.Open, FileAccess.Read))
                    {
                        md5.ComputeHash(fileStream);

                        foreach (byte b in md5.Hash)
                        {
                            sb.Append(string.Format("{0:X2}", b));
                        }
                    }
                }
                if (!sb.ToString().Equals(task.FileSignature))
                {
                    if (File.Exists(taskPackageFilePath))
                    {
                        File.Delete(taskPackageFilePath);
                    }

                    if (!this.DownloadTaskFile(task.Id, taskPackageFilePath))
                    {
                        TaskRuntimeInfo t = null;
                        this._taskInfos.TryRemove(task.Id, out t);
                        return null;
                    }
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

        private bool DownloadTaskFile(string taskId, string savePath)
        {
            var downloadResult = this._sdk.DownloadTaskFile(taskId);
            if (downloadResult.HasError)
            {
                this._logger.Error("download task file fail, taskId:{0} ; Message:{1}", taskId, downloadResult.ErrorMessage);
                return false;
            }
            else
            {
                downloadResult.Data.Seek(0, SeekOrigin.Begin);
                this._logger.Trace(downloadResult.Data.Length);
                DirectoryAndFileHelper.SaveFile(downloadResult.Data, savePath);
                return true;
            }
        }

        #endregion
    }
}
