using System.IO;
using TaskManager.DB;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;
using TaskManager.Service.Helper;

namespace TaskManager.Service.Service.Ta.TaskOperator.Queryer
{
    internal class TaskFileQueryerDependent : TmBaseDependentProvider
    {
        public TaskFileQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<ITaskRepository>();
        }
    }

    internal class TaskFileQueryer : TmQueryProcess<Stream>
    {
        private readonly string _taskId;

        private readonly ITaskRepository _taskRepository;

        public TaskFileQueryer(ITmProcessConfig config, string taskId)
            : base(config)
        {
            this._taskId = taskId;

            this._taskRepository = base.ResolveDependency<ITaskRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._taskId))
            {
                base.CacheProcessError("任务ID不可为空");
                return false;
            }
            if (!this._taskRepository.ExistById(this._taskId))
            {
                base.CacheProcessError("不存在该任务");
                return false;
            }

            return true;
        }

        protected override Stream Query()
        {
            var taskFilePath = SiteFileHelper.GetTaskFilePath(base.RootPath, this._taskId);
            if (!File.Exists(taskFilePath))
            {
                base.CacheProcessError("不存在任务文件");
                return null;
            }
            else
            {
                using (var fileStream = new FileStream(taskFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    return new MemoryStream(bytes);
                }
            }
        }
    }
}
