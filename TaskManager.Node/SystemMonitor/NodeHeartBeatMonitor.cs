namespace TaskManager.Node.SystemMonitor
{
    internal class NodeHeartBeatMonitor : BaseMonitor
    {
        public NodeHeartBeatMonitor(MonitorConfig config)
            : base(config)
        {
        }

        protected override void Monitoring()
        {
            var hbResult = base.Sdk.HeartBeat(base.NodeId);
            if (hbResult.HasError)
            {
                base.Logger.Error("HeartBear error:{0}", hbResult.ErrorMessage);
            }
        }

        protected override void BeforeMonitoring()
        {
            base.Logger.Trace("NodeHeartBeatMonitor before");
            base.BeforeMonitoring();
        }

        protected override void AfterMonitoring()
        {
            base.Logger.Trace("NodeHeartBeatMonitor after");
            base.AfterMonitoring();
        }
    }
}
