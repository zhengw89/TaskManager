using CommonProcess.DependentProvider;
using TaskManager.DB;

namespace TaskManager.Service.Core
{
    internal class TmProcessConfig : ITmProcessConfig
    {
        private readonly ITaskManagerDb _db;
        private readonly string _rootPath;

        public IDependentProvider DependentProvider { get; set; }
        public ITaskManagerDb Db { get { return this._db; } }
        public string RootPath { get { return this._rootPath; } }

        public TmProcessConfig(ITaskManagerDb db, string rootPath)
        {
            this._db = db;

            this._rootPath = rootPath;
        }
    }
}
