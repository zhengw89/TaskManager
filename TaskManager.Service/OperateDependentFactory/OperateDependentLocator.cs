using TaskManager.Service.Service.Org.OrgOperator.Operator;
using TaskManager.Service.Service.Org.OrgOperator.Queryer;

namespace TaskManager.Service.OperateDependentFactory
{
    internal class OperateDependentLocator
    {
        private static readonly OperateDependentLocator Instance = new OperateDependentLocator();

        private readonly IDependentContainer _container;
        public static IDependentContainer Container { get { return Instance._container; } }

        private OperateDependentLocator()
        {
            _container = new DependentContainer();
            RegisterDefaults(_container);
        }

        private void RegisterDefaults(IDependentContainer container)
        {
            this.RegistOrg(container);
        }

        private void RegistOrg(IDependentContainer container)
        {
            container.Register<LoginOperator>(db => new LoginOperatorDependent(db));
            container.Register<UserByIdQueryer>(db => new UserByIdQueryerDependent(db));
        }
    }
}
