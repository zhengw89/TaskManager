using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Ta;

namespace TaskManager.Repository.Repositories.MySql.Ta
{
    internal class MsTaskJobRepository : BaseTaskJobRepository
    {
        public MsTaskJobRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
