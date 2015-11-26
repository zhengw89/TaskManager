using System;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Dev.NodeHeartBeatOperator.Creator
{
    internal class NodeHeartBeatCreatorDependent : TmBaseDependentProvider
    {
        public NodeHeartBeatCreatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<INodeHeartBeatRepository>();
        }
    }

    internal class NodeHeartBeatCreator : TmOperateProcess
    {
        private readonly string _nodeId;

        private readonly INodeHeartBeatRepository _heartBeatRepository;

        public NodeHeartBeatCreator(ITmProcessConfig config, string nodeId)
            : base(config)
        {
            this._nodeId = nodeId;

            this._heartBeatRepository = base.ResolveDependency<INodeHeartBeatRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._nodeId))
            {
                base.CacheProcessError("节点ID为空");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            if (!this._heartBeatRepository.Create(new NodeHeartBeat()
            {
                BeatTime = DateTime.Now,
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                NodeId = this._nodeId,
                UpdateTime = DateTime.Now
            }))
            {
                base.CacheProcessError("创建心跳记录失败");
                return false;
            }

            return true;
        }
    }
}
