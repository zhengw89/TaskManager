namespace TaskManager.Service.Core
{
    internal abstract class TmOperateProcess : TmBaseCoreOperateProcess
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

        protected TmOperateProcess(ITmProcessConfig config)
            : base(config)
        {
            this._config = config;
        }

        public bool ExecuteProcess()
        {
            return base.ExecuteCoreProcess();
        }
    }
}
