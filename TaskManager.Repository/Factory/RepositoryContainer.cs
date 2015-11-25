using System;
using System.Collections.Concurrent;
using TaskManager.DB;

namespace TaskManager.Repository.Factory
{
    internal class RepositoryContainer : IRepositoryContainer
    {
        private readonly ConcurrentDictionary<Type, object> _factories;

        public RepositoryContainer()
        {
            _factories = new ConcurrentDictionary<Type, object>();
        }

        public void Register<TRepository>(Func<ITaskManagerDb, TRepository> factory)
        {
            Type key = typeof(TRepository);
            _factories[key] = factory;
        }

        public TRepository Resolve<TRepository>(ITaskManagerDb db)
        {
            object factory;

            if (_factories.TryGetValue(typeof(TRepository), out factory))
                return ((Func<ITaskManagerDb, TRepository>)factory)(db);

            throw new ArgumentException("*******RepositoryContainer unknow argument, not Register");
        }
    }
}
