using TaskManager.DB;

namespace TaskManager.Service
{
    public class ServiceConfig
    {
        private readonly TaskManagerDbType _dbType;
        private readonly string _connectionString, _connectionProvider;
        private readonly string _userId, _userName;
        private readonly string _rootPath;

        public TaskManagerDbType DbType { get { return this._dbType; } }
        public string ConnectionString { get { return this._connectionString; } }
        public string ConnetionProvider { get { return this._connectionProvider; } }
        public string UserId { get { return this._userId; } }
        public string UserName { get { return this._userName; } }
        public string RootPath { get { return this._rootPath; } }

        public ServiceConfig(string connectionString, string connectionProvider, TaskManagerDbType dbType, string userId, string userName, string rootPath)
        {
            this._connectionString = connectionString;
            this._connectionProvider = connectionProvider;
            this._dbType = dbType;

            this._userId = userId;
            this._userName = userName;

            this._rootPath = rootPath;
        }
    }
}
