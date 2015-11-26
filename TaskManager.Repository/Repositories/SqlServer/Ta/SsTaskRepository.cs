using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ta;

namespace TaskManager.Repository.Repositories.SqlServer.Ta
{
    internal class SsTaskRepository : BaseTaskRepository
    {
        public SsTaskRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
