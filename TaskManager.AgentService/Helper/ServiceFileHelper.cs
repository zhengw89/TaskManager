using System;

namespace TaskManager.AgentService.Helper
{
    internal class ServiceFileHelper
    {
        public static string GetTaskFilePath(string rootPath, string taskId)
        {
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException();
            }

            return string.Format("{0}{1}.zip", rootPath, taskId);
        }

        public static string GetTaskFileUnzipFolderPath(string rootPath, string taskId)
        {
            if (string.IsNullOrEmpty(rootPath) || string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException();
            }
            return string.Format("{0}\\{1}\\", rootPath, taskId);
        }
    }
}
