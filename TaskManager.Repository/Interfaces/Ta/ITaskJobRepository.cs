using System;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.LogicEntity.Enums.Ta;

namespace TaskManager.Repository.Interfaces.Ta
{
    public interface ITaskJobRepository
    {
        bool Exists(string jobId);

        bool Create(TaskJob taskJob);

        bool Update(string jobId, TaskJobStatus jobStatus, DateTime jobEndTime, string resultMessage, DateTime updateTime);

        TaskJob GetById(string jobId);

        TaskJobStatus GetJobStatusById(string jobId);

        PagedList<TaskJob> GetByCondition(string taskId, int pageIndex, int pageSize);
    }
}
