using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Dev;

namespace TaskManager.Repository.Repositories.SqlServer.Dev
{
    internal class SsNodeRepository : BaseNodeRepository
    {
        public SsNodeRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
