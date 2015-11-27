using System.Collections.Generic;
using System.Linq;
using TaskManager.ApiSdk.Factory;
using TaskManager.ApiSdk.Sdk;

namespace TaskManager.AgentService.SystemMonitor
{
    internal class MonitorManager
    {
        private const int HeartBeatMonitorInterval = 10 * 1000;

        private readonly static object LockObj = new object();
        private static MonitorManager _instance;
        public static MonitorManager Insatnce
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new MonitorManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private readonly List<IMonitor> _monitors;

        private MonitorManager()
        {
            this._monitors = new List<IMonitor>();
        }

        public void StartMonitoring(string host, string nodeId, bool needRecordLog)
        {
            if (!this._monitors.Any())
            {
                this._monitors.Add(new NodeHeartBeatMonitor(
                    new MonitorConfig(
                        nodeId,
                        HeartBeatMonitorInterval,
                        needRecordLog,
                        SdkFactory.CreateSdk(new SdkConfig(host)))));
            }

            foreach (var monitor in this._monitors)
            {
                monitor.StartMonitor();
            }
        }

        public void StopMonitoring()
        {
            foreach (var monitor in this._monitors)
            {
                monitor.StopMonitor();
            }
        }
    }
}
