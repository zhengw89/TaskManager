using System.Collections.Generic;
using System.Linq;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeHeartBeatOperator.Queryer
{
    internal class NodeLatestHeartBeadQueryerDependent : TmBaseDependentProvider
    {
        public NodeLatestHeartBeadQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeHeartBeatRepository>();
        }
    }

    internal class NodeLatestHeartBeadQueryer : TmQueryProcess<List<NodeHeartBeat>>
    {
        private readonly List<string> _nodeIds;

        private readonly INodeHeartBeatRepository _nodeHeartBeatRepository;

        public NodeLatestHeartBeadQueryer(ITmProcessConfig config, List<string> nodeIds)
            : base(config)
        {
            this._nodeIds = nodeIds;

            this._nodeHeartBeatRepository = base.ResolveDependency<INodeHeartBeatRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            return true;
        }

        protected override List<NodeHeartBeat> Query()
        {
            var result = new List<NodeHeartBeat>();
            if (this._nodeIds == null || !this._nodeIds.Any()) return result;

            foreach (var nodeId in this._nodeIds)
            {
                var hb = this._nodeHeartBeatRepository.GetLatestByNode(nodeId);
                if (hb != null) result.Add(hb);
            }

            return result;
        }
    }
}
