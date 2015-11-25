namespace TaskManager.Service.Core
{
    internal abstract class TmOperateProcess : TmBaseCoreOperateProcess
    {
        protected TmOperateProcess(ITmProcessConfig config)
            : base(config)
        {
        }

        public bool ExecuteProcess()
        {
            return base.ExecuteCoreProcess();
        }
    }
}
