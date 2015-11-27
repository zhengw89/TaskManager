using System.IO;
using System.Linq;

namespace TaskManager.Utils
{
    public class DirectoryAndFileHelper
    {
        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

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
    }
}
