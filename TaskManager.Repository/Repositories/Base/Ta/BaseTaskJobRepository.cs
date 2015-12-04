using System;
using TaskManager.DB;
using TaskManager.DBEntity.TA;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Ta;
using TaskManager.LogicEntity.Enums.Ta;
using TaskManager.Repository.Converter.Ta;
using TaskManager.Repository.Interfaces.Ta;

namespace TaskManager.Repository.Repositories.Base.Ta
{
    internal abstract class BaseTaskJobRepository : BaseRepository<TaskJob, T_TASK_JOB>, ITaskJobRepository
    {
        protected BaseTaskJobRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override TaskJob FromT(T_TASK_JOB t)
        {
            return t.FromT();
        }

        protected override T_TASK_JOB ToT(TaskJob l)
        {
            return l.ToT();
        }

        public bool Exists(string jobId)
        {
            return base.BaseQuery.Equal("TAJ_Id", jobId).Equal(IsActive, true).QueryCount() > 0;
        }

        public bool Create(TaskJob taskJob)
        {
            return base.Add(taskJob.ToT());
        }

        public bool Update(string jobId, TaskJobStatus jobStatus, DateTime jobEndTime, string resultMessage, DateTime updateTime)
        {
            var sql = new Sql("SET TAJ_Status = @0, TAJ_EndTime = @1, TAJ_Result = @2, UpdateTime = @3", (int)jobStatus, jobEndTime, resultMessage,
                updateTime);
            sql.Where("TAJ_Id = @0 AND IsActive = 1", jobId);
            return base.Update(sql);
        }

        public TaskJob GetById(string jobId)
        {
            return base.BaseQuery.Equal("TAJ_Id", jobId).Equal(IsActive, true).SingleOrDefault().FromT();
        }

        public TaskJobStatus GetJobStatusById(string jobId)
        {
            return (TaskJobStatus)base.BaseQuery.Equal("TAJ_Id", jobId).Equal(IsActive, true).SingleOrDefault().Status;
        }

        public PagedList<TaskJob> GetByCondition(string taskId, int pageIndex, int pageSize)
        {
            var bq = base.BaseQuery.Equal(IsActive, true);
            if (!string.IsNullOrEmpty(taskId))
            {
                bq.Equal("TA_Id", taskId);
            }
            bq.OrderBy("TAJ_ExecuteTime");
            return base.ConvertToPagedList(bq.QueryByPage(pageIndex, pageSize));
        }
    }
}
