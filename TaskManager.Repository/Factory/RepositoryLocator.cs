using System;
using TaskManager.DB;
using TaskManager.Repository.Factory.Register;

namespace TaskManager.Repository.Factory
{
    public class RepositoryLocator
    {
        private static readonly RepositoryLocator Instance = new RepositoryLocator();
        private readonly IRepositoryContainer _ssContainer;
        private readonly IRepositoryContainer _msContainer;

        private RepositoryLocator()
        {
            this._ssContainer = new RepositoryContainer();
            this._msContainer = new RepositoryContainer();

            var ssRepositoryContainerRegister = new SqlServerRepositoryContainerRegister();
            ssRepositoryContainerRegister.Register(this._ssContainer);

            var msRepositoryContainerRegister = new MySqlRepositoryContainerRegister();
            msRepositoryContainerRegister.Register(this._msContainer);
        }

        public static IRepositoryContainer GetContainer(TaskManagerDbType dbType)
        {
            switch (dbType)
            {
                case TaskManagerDbType.SqlServer:
                    return Instance._ssContainer;
                case TaskManagerDbType.MySql:
                    return Instance._msContainer;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
