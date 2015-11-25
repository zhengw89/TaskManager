using CommonProcess;

namespace TaskManager.Service.Core
{
    internal abstract class TmBaseCoreOperateProcess : CoreOperateProcess
    {
        private readonly ITmProcessConfig _config;

        protected TmBaseCoreOperateProcess(ITmProcessConfig config)
            : base(config)
        {
            this._config = config;
        }

        protected void CacheProcessError(string message)
        {
            base.CacheError(-100, message);
        }

        protected override void OnProcessStart()
        {
            this._config.Db.BeginTransaction();
            base.OnProcessStart();
        }

        protected override void OnProcessSuccess()
        {
            this._config.Db.CompleteTransaction();
            base.OnProcessSuccess();
        }

        protected override void OnProcessFail()
        {
            this._config.Db.AbortTransaction();
            base.OnProcessFail();
        }
    }
}
