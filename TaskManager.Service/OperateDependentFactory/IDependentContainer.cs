using System;
using CommonProcess;
using CommonProcess.DependentProvider;
using TaskManager.DB;
using TaskManager.Service.Core;

namespace TaskManager.Service.OperateDependentFactory
{
    internal interface IDependentContainer
    {
        void Register<TDependentSource>(Func<BaseDependentProvider> factory) where TDependentSource : DataProcess;

        void Register<TDependentSource>(Func<ITaskManagerDb, TmBaseDependentProvider> factory) where TDependentSource : DataProcess;

        TmBaseDependentProvider Resolve<TDependentSource>(ITaskManagerDb db) where TDependentSource : DataProcess;
    }
}
