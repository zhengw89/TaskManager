using TaskManager.DB;
using TaskManager.Repository.Factory;

namespace TaskManager.Service.Helper
{
    internal static class RepositoryHelper
    {
        public static T ResolveRepository<T>(ITaskManagerDb db)
        {
            return RepositoryLocator.GetContainer(db.DbType).Resolve<T>(db);
        }
    }
}
