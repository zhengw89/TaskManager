using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ta;

namespace TaskManager.Repository.Repositories.SqlServer.Ta
{
    internal class SsTaskJobRepository : BaseTaskJobRepository
    {
        public SsTaskJobRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
