using TaskManager.Service.Service.Dev.NodeHeartBeatOperator.Creator;
using TaskManager.Service.Service.Dev.NodeOperator.Creator;
using TaskManager.Service.Service.Dev.NodeOperator.Queryer;
using TaskManager.Service.Service.Org.UserOperator.Creator;
using TaskManager.Service.Service.Org.UserOperator.Operator;
using TaskManager.Service.Service.Org.UserOperator.Queryer;
using TaskManager.Service.Service.Org.UserOperator.Updater;
using TaskManager.Service.Service.Ta.TaskJobOperator.Operator;
using TaskManager.Service.Service.Ta.TaskJobOperator.Queryer;
using TaskManager.Service.Service.Ta.TaskOperator.Creator;
using TaskManager.Service.Service.Ta.TaskOperator.Queryer;

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
            this.RegistDev(container);
            this.RegistOrg(container);
            this.RegistTask(container);
        }

        private void RegistDev(IDependentContainer container)
        {
            container.Register<AllNodeQueryer>(db => new AllNodeQueryerDependent(db));
            container.Register<NodeByConditionQueryer>(db => new NodeByConditionQueryerDependent(db));
            container.Register<NodeCreator>(db => new NodeCreatorDependent(db));

            container.Register<NodeHeartBeatCreator>(db => new NodeHeartBeatCreatorDependent(db));
        }

        private void RegistOrg(IDependentContainer container)
        {
            container.Register<LoginOperator>(db => new LoginOperatorDependent(db));
            container.Register<UserByIdQueryer>(db => new UserByIdQueryerDependent(db));
            container.Register<UserByConditionQueryer>(db => new UserByConditionQueryerDependent(db));
            container.Register<UserCreator>(db => new UserCreatorDependent(db));
            container.Register<UserUpdater>(db => new UserUpdaterDependent(db));
        }

        private void RegistTask(IDependentContainer container)
        {
            container.Register<TaskByConditionQueryer>(db => new TaskByConditionQueryerDependent(db));
            container.Register<TaskCreator>(db => new TaskCreatorDependent(db));
            container.Register<TaskByNodeQueryer>(db => new TaskByNodeQueryerDependent(db));
            container.Register<StartTaskJobOperator>(db => new StartTaskJobOperatorDependent(db));
            container.Register<TaskFileQueryer>(db => new TaskFileQueryerDependent(db));
            container.Register<CompleteTaskJobOperator>(db => new CompleteTaskJobOperatorDependent(db));
            container.Register<TaskJobByConditionQueryer>(db => new TaskJobByConditionQueryerDependent(db));
        }
    }
}
