using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Repository.Interfaces.Dev;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Interfaces.Ta;
using TaskManager.Repository.Repositories.MySql.Dev;
using TaskManager.Repository.Repositories.MySql.Org;
using TaskManager.Repository.Repositories.MySql.Ta;

namespace TaskManager.Repository.Factory.Register
{
    internal class MySqlRepositoryContainerRegister : BaseRepositoryContainerRegister
    {
        protected override void RegisterDevRepositories(IRepositoryContainer container)
        {
            container.Register<INodeRepository>(db => new MsNodeRepository(db));
        }

        protected override void RegisterOrgRepositories(IRepositoryContainer container)
        {
            container.Register<IUserRepository>(db => new MsUserRepository(db));
        }

        protected override void RegisterTaRepositories(IRepositoryContainer container)
        {
            container.Register<ITaskRepository>(db => new MsTaskRepository(db));
        }

        protected override void RegisterUbRepositories(IRepositoryContainer container)
        {
        }
    }
}
