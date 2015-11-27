using System;
using System.Threading;
using NLog;
using TaskManager.ApiSdk.Sdk;

namespace TaskManager.AgentService.SystemMonitor
{
    internal abstract class BaseMonitor : IMonitor
    {
        private Thread _thread;

        private readonly MonitorConfig _config;
        private Logger _logger;

        protected string NodeId
        {
            get
            {
                if (this._config == null) return null;
                return this._config.NodeId;
            }
        }
        protected Logger Logger
        {
            get { return this._logger; }
        }
        protected ITmSdk Sdk
        {
            get
            {
                if (this._config == null) return null;
                return this._config.Sdk;
            }
        }


        protected BaseMonitor(MonitorConfig config)
        {
            this._config = config;
            this._logger = LogManager.GetLogger("MonitorLogger");

            this._thread = new Thread(RunMonitor)
            {
                IsBackground = true
            };
        }

        public void StartMonitor()
        {
            this._logger.Info("StartMonitor:{0}", this.GetType());
            this._thread.Start();
        }

        public void StopMonitor()
        {
            this._logger.Info("StopMonitor:{0}", this.GetType());
            this._thread.Abort();
        }

        public void Dispose()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
                this._thread = null;
            }
            this._logger = null;
        }

        protected virtual void BeforeMonitoring()
        {

        }

        protected abstract void Monitoring();

        protected virtual void AfterMonitoring()
        {

        }

        private void RunMonitor()
        {
            while (true)
            {
                try
                {
                    BeforeMonitoring();
                    Monitoring();
                    AfterMonitoring();
                    Thread.Sleep(this._config.Interval);
                }
                catch (Exception ex)
                {
                    this._logger.Error("monitor '{0}' error: {1}", GetType(), ex.Message);
                }
            }
        }
    }
}
