using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Repositories.SqlServer.Org;

namespace TaskManager.Repository.Factory.Register
{
    internal class SqlServerRepositoryContainerRegister : BaseRepositoryContainerRegister
    {
        protected override void RegisterOrgRepositories(IRepositoryContainer container)
        {
            container.Register<IUserRepository>(db => new SsUserRepository(db));
        }

        protected override void RegisterUbRepositories(IRepositoryContainer container)
        {
        }
    }
}
