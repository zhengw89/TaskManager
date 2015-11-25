using System;
using System.Collections.Concurrent;
using CommonProcess.DependentProvider;
using TaskManager.DB;
using TaskManager.Service.Core;

namespace TaskManager.Service.OperateDependentFactory
{
    internal class DependentContainer : IDependentContainer
    {
        private readonly ConcurrentDictionary<Type, object> _factories;

        public DependentContainer()
        {
            _factories = new ConcurrentDictionary<Type, object>();
        }

        public void Register<TDependentSource>(Func<BaseDependentProvider> factory)
        {
            Type key = typeof(TDependentSource);
            _factories[key] = factory;
        }

        public void Register<TDependentSource>(Func<ITaskManagerDb, TmBaseDependentProvider> factory)
        {
            Type key = typeof(TDependentSource);
            _factories[key] = factory;
        }

        public TmBaseDependentProvider Resolve<TDependentSource>(ITaskManagerDb db)
        {
            object factory;

            if (_factories.TryGetValue(typeof(TDependentSource), out factory))
                return ((Func<ITaskManagerDb, TmBaseDependentProvider>)factory)(db);

            throw new ArgumentOutOfRangeException();
        }
    }
}
