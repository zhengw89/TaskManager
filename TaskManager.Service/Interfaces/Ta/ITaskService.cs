using System.IO;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Service.Interfaces.Ta
{
    public interface ITaskService
    {
        TmProcessResult<bool> CreateTask(string name, string nodeId, string cron, string className, string methodName, string remark,
            string taskFileName, Stream taskFileStream);

        TmProcessResult<PagedList<Task>> GetByCondition(int pageIndex, int pageSize);
    }
}
