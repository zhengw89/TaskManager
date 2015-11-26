using System.Collections.Generic;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Repository.Interfaces.Ta
{
    public interface ITaskRepository
    {
        bool ExistById(string taskId);

        bool Create(Task task);

        PagedList<Task> GetByCondition(int pageIndex, int pageSize);

        List<Task> GetByNodeId(string nodeId);

        string GetNodeIdByTaskId(string taskId);
    }
}
