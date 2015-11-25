using System.Text;
using System.Web.Mvc;

namespace TaskManager.Helper.CustomResult
{
    public class DownloadResult : ActionResult
    {
        private readonly string _absolutePath;
        private readonly string _fileName;
        private readonly long _fileLength;

        public DownloadResult(string absolutePath, string fileName, long fileLengh)
        {
            this._absolutePath = absolutePath;
            this._fileName = fileName;
            this._fileLength = fileLengh;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                context.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                string name = this._fileName;
                name = System.Web.HttpUtility.UrlEncode(name, Encoding.UTF8);
                context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                context.HttpContext.Response.ContentType = "application/octet-stream";
                context.HttpContext.Response.AddHeader("Connection", "Keep-Alive");

                string filePath = _absolutePath;
                if (_fileLength != 0)
                {
                    context.HttpContext.Response.AddHeader("Content-Lenght", _fileLength.ToString());
                }

                context.HttpContext.Response.TransmitFile(filePath);
            }
            catch
            {
                context.HttpContext.ClearError();
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = 404;
            }
        }
    }
}