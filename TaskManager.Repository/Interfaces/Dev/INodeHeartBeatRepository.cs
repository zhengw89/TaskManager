using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Repository.Interfaces.Dev
{
    public interface INodeHeartBeatRepository
    {
        bool Create(NodeHeartBeat heartBeat);
    }
}
