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
    }
}
