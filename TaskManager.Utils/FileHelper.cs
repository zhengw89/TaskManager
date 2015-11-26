using System;
using System.IO;
using System.Linq;

namespace TaskManager.Utils
{
    public class FileHelper
    {
        public static bool SaveFile(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
            return true;
        }

        public static string GetExtNameByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;

            var re = fileName.Split('.');
            if (re.Count() < 2) return null;

            return re.Last();
        }

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
