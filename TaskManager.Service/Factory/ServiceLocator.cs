using TaskManager.Service.Interfaces.Org;
using TaskManager.Service.Service.Org;

namespace TaskManager.Service.Factory
{
    public class ServiceLocator
    {
        private static readonly ServiceLocator Instance = new ServiceLocator();
        private readonly IServiceContainer _container;

        public static IServiceContainer Container
        {
            get { return Instance._container; }
        }

        private ServiceLocator()
        {
            _container = new ServiceContainer();

            RegisterDefaults(_container);
        }

        private void RegisterDefaults(IServiceContainer container)
        {
            this.RegisterOrgService(container);
        }

        private void RegisterOrgService(IServiceContainer container)
        {
            container.Register<IUserService>(c => new UserService(c));
        }
    }
}
