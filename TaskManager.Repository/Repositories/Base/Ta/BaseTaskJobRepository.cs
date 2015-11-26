using TaskManager.DB;
using TaskManager.DBEntity.TA;
using TaskManager.LogicEntity.Entities.Ta;
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

        public bool Create(TaskJob taskJob)
        {
            return base.Add(taskJob.ToT());
        }
    }
}
