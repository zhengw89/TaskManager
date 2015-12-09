using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;
using TaskManager.Service.Helper;
using TaskManager.Utils;

namespace TaskManager.Service.Service.Ta.TaskOperator.Creator
{
    internal class TaskCreatorDependent : TmBaseDependentProvider
    {
        public TaskCreatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<ITaskRepository>();
        }
    }

    internal class TaskCreator : TmOperateProcess
    {
        private readonly string _name, _nodeId, _cron, _dllName, _className, _remark;
        private readonly Stream _taskFileStream;

        private readonly ITaskRepository _taskRepository;

        public TaskCreator(ITmProcessConfig config, string name, string nodeId, string cron, string dllName, string className, string remark,
            Stream taskFileStream)
            : base(config)
        {
            this._name = name;
            this._nodeId = nodeId;
            this._cron = cron;
            this._dllName = dllName;
            this._className = className;
            this._remark = remark;
            this._taskFileStream = taskFileStream;

            this._taskRepository = base.ResolveDependency<ITaskRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._name))
            {
                base.CacheProcessError("任务名称不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._cron))
            {
                base.CacheProcessError("CRON表达式不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._dllName))
            {
                base.CacheProcessError("DLL名称不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._className))
            {
                base.CacheProcessError("类名不可为空");
                return false;
            }
            if (this._taskFileStream == null)
            {
                base.CacheProcessError("任务文件为空");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            var taskId = Guid.NewGuid().ToString();

            var sb = new StringBuilder();
            using (var md5 = MD5.Create())
            {
                md5.ComputeHash(this._taskFileStream);

                foreach (byte b in md5.Hash)
                {
                    sb.Append(string.Format("{0:X2}", b));
                }
            }

            if (!this._taskRepository.Create(new Task()
            {
                CreateTime = DateTime.Now,
                Cron = this._cron,
                Id = taskId,
                IsActive = true,
                Name = this._name,
                NodeId = this._nodeId,
                UpdateTime = DateTime.Now,
                ClassName = this._className,
                Remark = this._remark,
                DllName = this._dllName,
                FileSignature = sb.ToString()
            }))
            {
                base.CacheProcessError("创建任务失败");
                return false;
            }

            var taskFilePath = SiteFileHelper.GetTaskFilePath(base.RootPath, taskId);
            if (!DirectoryAndFileHelper.SaveFile(this._taskFileStream, taskFilePath))
            {
                base.CacheProcessError("任务运行文件保存失败，创建任务失败");
                return false;
            }

            return true;
        }
    }
}
