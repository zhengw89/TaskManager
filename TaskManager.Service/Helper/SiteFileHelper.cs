using System;

namespace TaskManager.Service.Helper
{
    internal class SiteFileHelper
    {
        public static string GetTaskFilePath(string rootPath, string taskId)
        {
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException();
            }

            return string.Format("{0}Upload\\TaskFile\\{1}.zip", rootPath, taskId);
        }
    }
}
