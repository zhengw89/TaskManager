namespace TaskManager.Service.Core
{
    internal abstract class TmOperateProcessWithResult<T> : TmBaseCoreOperateProcess
    {
        protected TmOperateProcessWithResult(ITmProcessConfig config)
            : base(config)
        {
        }

        protected abstract T GetResult();

        public T ExecuteProcess()
        {
            return base.ExecuteCoreProcess() ? GetResult() : default(T);
        }
    }
}
