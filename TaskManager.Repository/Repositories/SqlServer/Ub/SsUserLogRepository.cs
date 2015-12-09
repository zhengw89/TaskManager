using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ub;

namespace TaskManager.Repository.Repositories.SqlServer.Ub
{
    internal class SsUserLogRepository : BaseUserLogRepository
    {
        public SsUserLogRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
