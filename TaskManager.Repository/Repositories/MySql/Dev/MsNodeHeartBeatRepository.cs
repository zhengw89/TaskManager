using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Dev;

namespace TaskManager.Repository.Repositories.MySql.Dev
{
    internal class MsNodeHeartBeatRepository : BaseNodeHeartBeatRepository
    {
        public MsNodeHeartBeatRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
