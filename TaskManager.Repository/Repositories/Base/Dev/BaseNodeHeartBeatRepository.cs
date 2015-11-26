using TaskManager.DB;
using TaskManager.DBEntity.DEV;
using TaskManager.LogicEntity.Entities.Dev;
using TaskManager.Repository.Converter.Dev;
using TaskManager.Repository.Interfaces.Dev;

namespace TaskManager.Repository.Repositories.Base.Dev
{
    internal abstract class BaseNodeHeartBeatRepository : BaseRepository<NodeHeartBeat, T_DEV_NODE_HEART_BEAT>, INodeHeartBeatRepository
    {
        protected BaseNodeHeartBeatRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override NodeHeartBeat FromT(T_DEV_NODE_HEART_BEAT t)
        {
            return t.FromT();
        }

        protected override T_DEV_NODE_HEART_BEAT ToT(NodeHeartBeat l)
        {
            return l.ToT();
        }

        public bool Create(NodeHeartBeat heartBeat)
        {
            return base.Add(heartBeat.ToT());
        }
    }
}
