﻿using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Repository.Interfaces.Ub;
using TaskManager.Repository.Repositories.SqlServer.Dev;
using TaskManager.Repository.Repositories.SqlServer.Org;
using TaskManager.Repository.Repositories.SqlServer.Ta;
using TaskManager.Repository.Repositories.SqlServer.Ub;

namespace TaskManager.Repository.Factory.Register
{
    internal class SqlServerRepositoryContainerRegister : BaseRepositoryContainerRegister
    {
        protected override void RegisterDevRepositories(IRepositoryContainer container)
        {
            container.Register<INodeRepository>(db => new SsNodeRepository(db));
            container.Register<INodeHeartBeatRepository>(db => new SsNodeHeartBeatRepository(db));
        }

        protected override void RegisterOrgRepositories(IRepositoryContainer container)
        {
            container.Register<IUserRepository>(db => new SsUserRepository(db));
        }

        protected override void RegisterTaRepositories(IRepositoryContainer container)
        {
            container.Register<ITaskRepository>(db => new SsTaskRepository(db));
            container.Register<ITaskJobRepository>(db => new SsTaskJobRepository(db));
        }

        protected override void RegisterUbRepositories(IRepositoryContainer container)
        {
            container.Register<IUserLogRepository>(db => new SsUserLogRepository(db));
        }
    }
}
