using CommonProcess.Error;
using TaskManager.LogicEntity;

namespace TaskManager.Service.Core
{
    internal static class ErrorConverter
    {
        public static TmProcessError ToTmProcessError(this ProcessError error)
        {
            if (error == null) return null;
            return new TmProcessError(error.Code, error.Message);
        }
    }
}
