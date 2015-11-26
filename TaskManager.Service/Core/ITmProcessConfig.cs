using CommonProcess;
using TaskManager.DB;

namespace TaskManager.Service.Core
{
    internal interface ITmProcessConfig : IDataProcessConfig
    {
        ITaskManagerDb Db { get; }

        string RootPath { get; }
    }
}
