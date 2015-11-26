using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Service.Interfaces.Dev;
using TaskManager.Service.Service.Dev.NodeOperator.Creator;
using TaskManager.Service.Service.Dev.NodeOperator.Queryer;

namespace TaskManager.Service.Service.Dev
{
    internal class DevService : BaseService, INodeService
    {
        public DevService(ServiceConfig config)
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
    }
}
