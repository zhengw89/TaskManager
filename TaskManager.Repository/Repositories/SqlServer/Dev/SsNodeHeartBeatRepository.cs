using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Dev;

namespace TaskManager.Repository.Repositories.SqlServer.Dev
{
    internal class SsNodeHeartBeatRepository : BaseNodeHeartBeatRepository
    {
        public SsNodeHeartBeatRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
