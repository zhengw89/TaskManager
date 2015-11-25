using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Repositories.MySql.Org;

namespace TaskManager.Repository.Factory.Register
{
    internal class MySqlRepositoryContainerRegister : BaseRepositoryContainerRegister
    {
        protected override void RegisterOrgRepositories(IRepositoryContainer container)
        {
            container.Register<IUserRepository>(db => new MsUserRepository(db));
        }

        protected override void RegisterUbRepositories(IRepositoryContainer container)
        {
        }
    }
}
