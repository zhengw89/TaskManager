using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Repository.Interfaces.Ta
{
    public interface ITaskJobRepository
    {
        bool Create(TaskJob taskJob);
    }
}
