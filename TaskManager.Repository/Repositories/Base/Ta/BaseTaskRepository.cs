using System.Collections.Generic;
using TaskManager.DB;
using TaskManager.DBEntity.TA;
using TaskManager.LogicEntity.Entities;
using TaskManager.Repository.Converter.Ta;
using TaskManager.Repository.Interfaces.Ta;
using Task = TaskManager.LogicEntity.Entities.Ta.Task;

namespace TaskManager.Repository.Repositories.Base.Ta
{
    internal abstract class BaseTaskRepository : BaseRepository<Task, T_TASK>, ITaskRepository
    {
        protected BaseTaskRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override Task FromT(T_TASK t)
        {
            return t.FromT();
        }

        protected override T_TASK ToT(Task l)
        {
            return l.ToT();
        }

        public bool ExistById(string taskId)
        {
            return base.BaseQuery.Equal(IsActive, true).Equal("TA_Id", taskId).QueryCount() > 0;
        }

        public bool Create(Task task)
        {
            return base.Add(task.ToT());
        }

        public PagedList<Task> GetByCondition(int pageIndex, int pageSize)
        {
            return
                base.ConvertToPagedList(base.BaseQuery.Equal(IsActive, true)
                    .OrderBy(CreateTime)
                    .QueryByPage(pageIndex, pageSize));
        }

        public List<Task> GetByNodeId(string nodeId)
        {
            return base.ConvertToList(base.BaseQuery.Equal("NODE_Id", nodeId).Equal(IsActive, true).Query());
        }

        public string GetNodeIdByTaskId(string taskId)
        {
            var queryResult = base.BaseQuery.Equal("TA_Id", taskId).Equal(IsActive, true).SingleOrDefault("NODE_Id");
            if (queryResult == null) return null;
            return queryResult.NodeId;
        }
    }
}
