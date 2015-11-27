using CommonProcess;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Ta.TaskOperator.Queryer
{
    internal class TaskByConditionQueryerDependent : TmBaseDependentProvider
    {
        public TaskByConditionQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<ITaskRepository>();
        }
    }

    internal class TaskByConditionQueryer : TmQueryProcess<PagedList<Task>>
    {
        private readonly int _pageIndex, _pageSize;

        private readonly ITaskRepository _taskRepository;

        public TaskByConditionQueryer(ITmProcessConfig config, int pageIndex, int pageSize)
            : base(config)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;

            this._taskRepository = base.ResolveDependency<ITaskRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (this._pageIndex < 0 || this._pageSize < 0)
            {
                base.CacheProcessError("分页参数错误");
                return false;
            }

            return true;
        }

        protected override PagedList<Task> Query()
        {
            return this._taskRepository.GetByCondition(this._pageIndex, this._pageSize);
        }
    }
}
