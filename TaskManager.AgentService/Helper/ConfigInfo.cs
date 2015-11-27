using System.Configuration;

namespace TaskManager.AgentService.Helper
{
    internal class ConfigInfo
    {
        private const string NodeIdConfigName = "NodeId",
            HostConfigName = "Host",
            TaskFileSaveRootPathConfigName = "TaskFileSaveRootPath";

        private static readonly object LockObj = new object();
        private static ConfigInfo _instance;
        public static ConfigInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigInfo();
                        }
                    }
                }

                return _instance;
            }
        }

        private string _nodeId;
        private string _host;
        private string _taskFileSaveRootPath;
        public string NodeId { get { return this._nodeId; } }
        public string Host { get { return this._host; } }
        public string TaskFileSaveRootPath { get { return this._taskFileSaveRootPath; } }

        private ConfigInfo()
        {
            InitConfigInfo();
        }

        private void InitConfigInfo()
        {
            this._nodeId = ConfigurationManager.AppSettings[NodeIdConfigName];
            this._host = ConfigurationManager.AppSettings[HostConfigName];

            var tempPath = ConfigurationManager.AppSettings[TaskFileSaveRootPathConfigName];
            if (!tempPath.EndsWith("\\"))
            {
                this._taskFileSaveRootPath = tempPath + "\\";
            }
            else
            {
                this._taskFileSaveRootPath = tempPath;
            }
        }
    }
}
