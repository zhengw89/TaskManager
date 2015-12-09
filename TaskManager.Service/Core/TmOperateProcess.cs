using System;

namespace TaskManager.Service.Core
{
    internal abstract class TmOperateProcess : TmBaseCoreOperateProcess
    {
        private readonly ITmProcessConfig _config;

        protected string RootPath
        {
            get
            {
                return this._config.RootPath;
            }
        }

        protected string UserId
        {
            get
            {
                return this._config.UserId;
            }
        }

        protected TmOperateProcess(ITmProcessConfig config)
            : base(config)
        {
            if (config == null) throw new ArgumentNullException();
            this._config = config;
        }

        public bool ExecuteProcess()
        {
            return base.ExecuteCoreProcess();
        }
    }
}
