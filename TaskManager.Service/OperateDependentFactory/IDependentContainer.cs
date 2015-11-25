using System;
using CommonProcess.DependentProvider;
using TaskManager.DB;
using TaskManager.Service.Core;

namespace TaskManager.Service.OperateDependentFactory
{
    internal interface IDependentContainer
    {
        void Register<TDependentSource>(Func<BaseDependentProvider> factory);

        void Register<TDependentSource>(Func<ITaskManagerDb, TmBaseDependentProvider> factory);

        TmBaseDependentProvider Resolve<TDependentSource>(ITaskManagerDb db);
    }
}
