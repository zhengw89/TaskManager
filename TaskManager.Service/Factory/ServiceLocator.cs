using TaskManager.Service.Interfaces.Dev;
using TaskManager.Service.Interfaces.Org;
using TaskManager.Service.Interfaces.Ta;
using TaskManager.Service.Service.Dev;
using TaskManager.Service.Service.Org;
using TaskManager.Service.Service.Ta;

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
            this.RegisterDevService(container);
            this.RegisterOrgService(container);
            this.RegisterTaskService(container);
        }

        private void RegisterDevService(IServiceContainer container)
        {
            container.Register<INodeService>(c => new DevService(c));
        }

        private void RegisterOrgService(IServiceContainer container)
        {
            container.Register<IUserService>(c => new UserService(c));
        }

        private void RegisterTaskService(IServiceContainer container)
        {
            container.Register<ITaskService>(c => new TaskService(c));
        }
    }
}
