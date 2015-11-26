using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ta;

namespace TaskManager.Repository.Repositories.MySql.Ta
{
    internal class MsTaskRepository : BaseTaskRepository
    {
        public MsTaskRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
