using CommonProcess.DependentProvider;
using TaskManager.DB;

namespace TaskManager.Service.Core
{
    internal class TmProcessConfig : ITmProcessConfig
    {
        private readonly ITaskManagerDb _db;

        public IDependentProvider DependentProvider { get; set; }
        public ITaskManagerDb Db { get { return this._db; } }

        public TmProcessConfig(ITaskManagerDb db)
        {
            this._db = db;
        }
    }
}
