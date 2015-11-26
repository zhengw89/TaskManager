using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Dev;

namespace TaskManager.Repository.Repositories.MySql.Dev
{
    internal class MsNodeRepository : BaseNodeRepository
    {
        public MsNodeRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
