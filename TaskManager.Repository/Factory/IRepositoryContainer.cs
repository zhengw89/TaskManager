using System;
using TaskManager.DB;

namespace TaskManager.Repository.Factory
{
    public interface IRepositoryContainer
    {
        void Register<TRepository>(Func<ITaskManagerDb, TRepository> factory);

        TRepository Resolve<TRepository>(ITaskManagerDb db);
    }
}
