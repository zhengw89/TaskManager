using TaskManager.ApiSdk.Sdk;

namespace TaskManager.AgentService.SystemMonitor
{
    public class MonitorConfig
    {
        private readonly string _nodeId;
        private readonly int _interval;
        private readonly bool _needRecordLog;
        private readonly ITmSdk _sdk;

        public string NodeId { get { return this._nodeId; } }
        public int Interval { get { return this._interval; } }
        public bool NeedRecordLog { get { return this._needRecordLog; } }
        public ITmSdk Sdk{get { return this._sdk; }}

        public MonitorConfig(string nodeId, int innterval, bool needRecordLog, ITmSdk sdk)
        {
            this._nodeId = nodeId;
            this._interval = innterval;
            this._needRecordLog = needRecordLog;
            this._sdk = sdk;
        }
    }
}
