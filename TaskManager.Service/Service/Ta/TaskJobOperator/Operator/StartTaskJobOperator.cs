using System;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.LogicEntity.Enums.Ta;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Ta.TaskJobOperator.Operator
{
    internal class StartTaskJobOperatorDependent : TmBaseDependentProvider
    {
        public StartTaskJobOperatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
            base.RegistRepository<ITaskRepository>();
            base.RegistRepository<ITaskJobRepository>();
        }
    }

    internal class StartTaskJobOperator : TmOperateProcessWithResult<string>
    {
        private readonly string _nodeId, _taskId;

        private readonly INodeRepository _nodeRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskJobRepository _taskJobRepository;

        private string _jobId;

        public StartTaskJobOperator(ITmProcessConfig config, string nodeId, string taskId)
            : base(config)
        {
            this._nodeId = nodeId;
            this._taskId = taskId;

            this._nodeRepository = base.ResolveDependency<INodeRepository>();
            this._taskRepository = base.ResolveDependency<ITaskRepository>();
            this._taskJobRepository = base.ResolveDependency<ITaskJobRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点ID不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._taskId))
            {
                base.CacheProcessError("任务ID不可为空");
                return false;
            }
            if (!this._nodeRepository.ExistById(this._nodeId))
            {
                base.CacheProcessError("不存在指定节点");
                return false;
            }
            if (!this._taskRepository.ExistById(this._taskId))
            {
                base.CacheProcessError("不存在指定任务");
                return false;
            }
            var nodeId = this._taskRepository.GetNodeIdByTaskId(this._taskId);
            if (!this._nodeId.Equals(nodeId))
            {
                base.CacheProcessError("该节点无执行该任务的权限");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            this._jobId = Guid.NewGuid().ToString();

            if (!this._taskJobRepository.Create(new TaskJob()
            {
                CreateTime = DateTime.Now,
                ExecuteTime = DateTime.Now,
                Id = this._jobId,
                IsActive = true,
                NodeId = this._nodeId,
                Status = TaskJobStatus.Executing,
                TaskId = this._taskId,
                UpdateTime = DateTime.Now
            }))
            {
                base.CacheProcessError("开始任务记录错误，开始失败");
                return false;
            }

            return true;
        }

        protected override string GetResult()
        {
            return this._jobId;
        }
    }
}
