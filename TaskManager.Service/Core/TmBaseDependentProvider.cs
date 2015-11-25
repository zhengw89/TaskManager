using CommonProcess.DependentProvider;
using TaskManager.DB;
using TaskManager.Service.Helper;

namespace TaskManager.Service.Core
{
    internal abstract class TmBaseDependentProvider : BaseDependentProvider
    {
        protected readonly ITaskManagerDb Db;

        protected TmBaseDependentProvider(ITaskManagerDb db)
        {
            this.Db = db;
        }

        protected void RegistRepository<T>()
        {
            base.RegisterDependent<T>(RepositoryHelper.ResolveRepository<T>(this.Db));
        }

        protected T ResolveRepository<T>()
        {
            return RepositoryHelper.ResolveRepository<T>(this.Db);
        }
    }
}
