using TaskManager.DB;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Ta.TaskJobOperator.Queryer
{
    internal class TaskJobByConditionQueryerDependent : TmBaseDependentProvider
    {
        public TaskJobByConditionQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<ITaskJobRepository>();
        }
    }

    internal class TaskJobByConditionQueryer : TmQueryProcess<PagedList<TaskJob>>
    {
        private readonly string _taskId;
        private readonly int _pageIndex, _pageSize;

        private readonly ITaskJobRepository _taskJobRepository;

        public TaskJobByConditionQueryer(ITmProcessConfig config, string taskId, int pageIndex, int pageSize)
            : base(config)
        {
            this._taskId = taskId;
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;

            this._taskJobRepository = base.ResolveDependency<ITaskJobRepository>();
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

        protected override PagedList<TaskJob> Query()
        {
            return this._taskJobRepository.GetByCondition(this._taskId, this._pageIndex, this._pageSize);
        }
    }
}
