using CommonProcess;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeOperator.Queryer
{
    internal class NodeByConditionQueryerDependent : TmBaseDependentProvider
    {
        public NodeByConditionQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
        }
    }

    internal class NodeByConditionQueryer : TmQueryProcess<PagedList<Node>>
    {
        private readonly int _pageIndex, _pageSize;

        private readonly INodeRepository _nodeRepository;

        public NodeByConditionQueryer(ITmProcessConfig config, int pageIndex, int pageSize)
            : base(config)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;

            this._nodeRepository = base.ResolveDependency<INodeRepository>();
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

        protected override PagedList<Node> Query()
        {
            return this._nodeRepository.GetByCondition(this._pageIndex, this._pageSize);
        }
    }
}
