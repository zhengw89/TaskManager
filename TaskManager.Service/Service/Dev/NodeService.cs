using System.Collections.Generic;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Service.Interfaces.Dev;
using TaskManager.Service.Service.Dev.NodeHeartBeatOperator.Creator;
using TaskManager.Service.Service.Dev.NodeHeartBeatOperator.Queryer;
using TaskManager.Service.Service.Dev.NodeOperator.Creator;
using TaskManager.Service.Service.Dev.NodeOperator.Queryer;

namespace TaskManager.Service.Service.Dev
{
    internal class NodeService : BaseService, INodeService
    {
        public NodeService(ServiceConfig config)
            : base(config)
        {
        }

        public TmProcessResult<bool> Create(string nodeName, string ip, int port, string remark)
        {
            return base.ExeProcess(db =>
            {
                var creator = new NodeCreator(
                    base.ResloveProcessConfig<NodeCreator>(db),
                    nodeName, ip, port, remark);

                return base.ExeOperateProcess(creator);
            });
        }

        public TmProcessResult<PagedList<Node>> GetByCondition(int pageIndex, int pageSize)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new NodeByConditionQueryer(
                    base.ResloveProcessConfig<NodeByConditionQueryer>(db),
                    pageIndex, pageSize);

                return base.ExeQueryProcess(queryer);
            });
        }

        public TmProcessResult<List<Node>> GetAllNode()
        {
            return base.ExeProcess(db =>
            {
                var queryer = new AllNodeQueryer(base.ResloveProcessConfig<AllNodeQueryer>(db));

                return base.ExeQueryProcess(queryer);
            });
        }

        public TmProcessResult<bool> CreateNodeHeartBeat(string nodeId)
        {
            return base.ExeProcess(db =>
            {
                var creator = new NodeHeartBeatCreator(
                    base.ResloveProcessConfig<NodeHeartBeatCreator>(db),
                    nodeId);

                return base.ExeOperateProcess(creator);
            });
        }

        public TmProcessResult<List<NodeHeartBeat>> GetLatestHeartBeat(List<string> nodeIds)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new NodeLatestHeartBeadQueryer(
                    base.ResloveProcessConfig<NodeLatestHeartBeadQueryer>(db),
                    nodeIds);

                return base.ExeQueryProcess(queryer);
            });
        }
    }
}
