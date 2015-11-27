using CommonProcess;

namespace TaskManager.Service.Core
{
    internal abstract class TmQueryProcess<T> : QueryProcess<T>
    {
        private readonly ITmProcessConfig _config;

        protected string RootPath
        {
            get
            {
                if (this._config == null) return null;
                return this._config.RootPath;
            }
        }

        protected TmQueryProcess(ITmProcessConfig config)
            : base(config)
        {
            this._config = config;
        }

        protected void CacheProcessError(string message)
        {
            base.CacheError(-100, message);
        }
    }
}
