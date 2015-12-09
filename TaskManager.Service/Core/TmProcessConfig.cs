using CommonProcess.DependentProvider;
using TaskManager.DB;

namespace TaskManager.Service.Core
{
    internal class TmProcessConfig : ITmProcessConfig
    {
        private readonly ITaskManagerDb _db;
        private readonly string _rootPath;
        private readonly string _userId;

        public IDependentProvider DependentProvider { get; set; }
        public ITaskManagerDb Db { get { return this._db; } }
        public string RootPath { get { return this._rootPath; } }
        public string UserId { get { return this._userId; } }

        public TmProcessConfig(ITaskManagerDb db, string rootPath, string userId)
        {
            this._db = db;

            this._rootPath = rootPath;
            this._userId = userId;
        }
    }
}
