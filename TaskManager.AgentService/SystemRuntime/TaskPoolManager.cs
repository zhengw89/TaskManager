using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using NLog;
using TaskManager.AgentService.Helper;
using TaskManager.ApiSdk.Factory;
using TaskManager.ApiSdk.Sdk;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Utils;

namespace TaskManager.AgentService.SystemRuntime
{
    internal class TaskPoolManager
    {
        private readonly Logger _logger;

        private string _nodeId;
        private string _rootPath;
        private ITmSdk _sdk;
        private readonly ConcurrentDictionary<string, Task> _taskInfos;

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

            this._taskInfos = new ConcurrentDictionary<string, Task>();
        }

        public void Init(string host, string nodeId, string rootPath)
        {
            this._nodeId = nodeId;
            this._rootPath = rootPath;
            this._sdk = SdkFactory.CreateSdk(new SdkConfig(host));

            this._taskInfos.Clear();

            this.LoadTasks();
        }

        private bool LoadTasks()
        {
            var loadTaskResult = this._sdk.GetTasks(this._nodeId);
            if (loadTaskResult.HasError)
            {
                return false;
            }

            if (!this.LoadTaskFile(loadTaskResult.Data)) return false;

            return true;
        }

        private bool LoadTaskFile(IEnumerable<Task> tasks)
        {
            foreach (var task in tasks)
            {
                this._logger.Trace("init task:{0}", task.Id);
                if (!this._taskInfos.ContainsKey(task.Id))
                {
                    if (!this._taskInfos.TryAdd(task.Id, task))
                    {
                        this._logger.Error("add task fail, taskId:{0}", task.Id);
                    }
                    else
                    {
                        var taskFilePath = ServiceFileHelper.GetTaskFilePath(this._rootPath, task.Id);
                        this._logger.Trace("task file path:{0}", taskFilePath);
                        if (!File.Exists(taskFilePath))
                        {
                            var downloadResult = this._sdk.DownloadTaskFile(task.Id);
                            if (downloadResult.HasError)
                            {
                                this._logger.Error("download task file fail, taskId:{0}", task.Id);
                                Task t = null;
                                this._taskInfos.TryRemove(task.Id, out t);
                            }
                            else
                            {
                                downloadResult.Data.Seek(0, SeekOrigin.Begin);
                                this._logger.Trace(downloadResult.Data.Length);
                                DirectoryAndFileHelper.SaveFile(downloadResult.Data, taskFilePath);
                            }
                        }
                        var taskFileUnzipFolderPath = ServiceFileHelper.GetTaskFileUnzipFolderPath(this._rootPath,
                                task.Id);
                        if (!Directory.Exists(taskFileUnzipFolderPath))
                        {
                            Directory.CreateDirectory(taskFileUnzipFolderPath);
                            CompressHelper.UnZip(taskFilePath, taskFileUnzipFolderPath);
                        }
                    }
                }
            }
            return true;
        }

        public void AddTask()
        {
            throw new NotImplementedException();
        }

        public void RemoveTask()
        {
            throw new NotImplementedException();
        }
    }
}
