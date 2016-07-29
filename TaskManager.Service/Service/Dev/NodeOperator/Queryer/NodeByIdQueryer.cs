using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeOperator.Queryer
{
    internal class NodeByIdQueryerDependent : TmBaseDependentProvider
    {
        public NodeByIdQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeRepository>();
        }
    }

    internal class NodeByIdQueryer : TmQueryProcess<Node>
    {
        private readonly string _nodeId;

        private readonly INodeRepository _nodeRepository;

        public NodeByIdQueryer(ITmProcessConfig config, string nodeId)
            : base(config)
        {
            this._nodeId = nodeId;
            this._nodeRepository = base.ResolveDependency<INodeRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点ID不可为空");
                return false;
            }

            return true;
        }

        protected override Node Query()
        {
            return this._nodeRepository.GetById(this._nodeId);
        }
    }
}
