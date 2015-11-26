using System.Collections.Generic;
using CommonProcess;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Ta.TaskOperator.Queryer
{
    internal class TaskByNodeQueryerDependent : TmBaseDependentProvider
    {
        public TaskByNodeQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
            base.RegistRepository<ITaskRepository>();
        }
    }

    internal class TaskByNodeQueryer : TmQueryProcess<List<Task>>
    {
        private readonly string _nodeId;

        private readonly INodeRepository _nodeRepository;
        private readonly ITaskRepository _taskRepository;

        public TaskByNodeQueryer(IDataProcessConfig config, string nodeId)
            : base(config)
        {
            this._nodeId = nodeId;

            this._nodeRepository = base.ResolveDependency<INodeRepository>();
            this._taskRepository = base.ResolveDependency<ITaskRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点ID不可为空");
                return false;
            }
            if (!this._nodeRepository.ExistById(this._nodeId))
            {
                base.CacheProcessError("节点不存在");
                return false;
            }

            return true;
        }

        protected override List<Task> Query()
        {
            return this._taskRepository.GetByNodeId(this._nodeId);
        }
    }
}
