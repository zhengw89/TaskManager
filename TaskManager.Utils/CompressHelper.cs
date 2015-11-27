using SharpCompress.Archive;
using SharpCompress.Common;

namespace TaskManager.Utils
{
    public class CompressHelper
    {
        /// <summary>
        /// 解压zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        public static void UnZip(string compressfilepath, string uncompressdir)
        {
            using (var archive = ArchiveFactory.Open(compressfilepath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(uncompressdir, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                }
            }
        }
    }
}
