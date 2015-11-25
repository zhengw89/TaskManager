using CommonProcess;

namespace TaskManager.Service.Core
{
    internal abstract class TmQueryProcess<T> : QueryProcess<T>
    {
        protected TmQueryProcess(IDataProcessConfig config)
            : base(config)
        {
        }

        protected void CacheProcessError(string message)
        {
            base.CacheError(-100, message);
        }
    }
}
