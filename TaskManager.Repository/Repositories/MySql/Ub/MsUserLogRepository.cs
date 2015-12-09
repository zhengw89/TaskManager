using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ub;

namespace TaskManager.Repository.Repositories.MySql.Ub
{
    internal class MsUserLogRepository : BaseUserLogRepository
    {
        public MsUserLogRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
