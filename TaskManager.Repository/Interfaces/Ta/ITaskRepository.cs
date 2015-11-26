using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Repository.Interfaces.Ta
{
    public interface ITaskRepository
    {
        bool Create(Task task);

        PagedList<Task> GetByCondition(int pageIndex, int pageSize);
    }
}
