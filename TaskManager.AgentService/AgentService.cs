using System.ServiceProcess;

namespace TaskManager.AgentService
{
    public partial class AgentService : ServiceBase
    {
        //private readonly Logger _logger = LogManager.GetLogger("AgentLogger");

        public AgentService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //_logger.Info("Start service");

            //try
            //{
            //    DirectoryAndFileHelper.CreateDirectoryIfNotExists(ConfigInfo.Instance.TaskFileSaveRootPath);
            //    _logger.Info("Init folder success");
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error("Init folder fail ex:{0}", ex.Message);
            //}

            //try
            //{
            //    MonitorManager.Insatnce.StartMonitoring(ConfigInfo.Instance.Host, ConfigInfo.Instance.NodeId, true);
            //    _logger.Info("Start service monitor");
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error("Start service monitor fail ex:{0}", ex.Message);
            //}

            //try
            //{
            //    TaskPoolManager.Instance.Init(ConfigInfo.Instance.Host, ConfigInfo.Instance.NodeId, ConfigInfo.Instance.TaskFileSaveRootPath);
            //    _logger.Info("TaskPool load task complete");
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error("TaskPool load task fail ex:{0}", ex.Message);
            //}
        }

        protected override void OnStop()
        {
            //_logger.Info("Stop service");
        }
    }
}
