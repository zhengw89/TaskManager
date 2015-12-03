using System.Collections.Generic;
using System.IO;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Service.Interfaces.Ta
{
    public interface ITaskService
    {
        TmProcessResult<bool> CreateTask(string name, string nodeId, string cron, string dllName, string className, string remark,
            string taskFileName, Stream taskFileStream);

        TmProcessResult<PagedList<Task>> GetByCondition(int pageIndex, int pageSize);

        TmProcessResult<List<Task>> GetByNode(string nodeId);

        TmProcessResult<string> StartTaskJob(string nodeId, string taskId);

        TmProcessResult<bool> CompleteTaskJob(string jobId, bool success, string result);

        TmProcessResult<Stream> GetTaskFile(string taskId);
    }
}
