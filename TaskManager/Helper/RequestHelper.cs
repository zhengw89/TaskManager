using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace TaskManager.Helper
{
    public class RequestHelper
    {
        public static string GetRequestContent(HttpContextBase httpContext)
        {
            httpContext.Request.InputStream.Position = 0;
            var contentEncoding = httpContext.Request.Headers["Content-Encoding"];

            string data = null;
            if (String.IsNullOrEmpty(contentEncoding))
            {
                data = GetStringFromStream(httpContext.Request.InputStream);
            }
            else if (contentEncoding.ToUpper().Contains("GZIP"))
            {
                data = GetStringFromGZipStream(httpContext.Request.InputStream);
            }
            else
            {
                data = GetStringFromStream(httpContext.Request.InputStream);
            }
            httpContext.Request.InputStream.Position = 0;
            return data;
        }

        private static string GetStringFromGZipStream(Stream stream)
        {
            string data = null;
            using (var desStream = new MemoryStream())
            {
                stream.CopyTo(desStream);
                desStream.Position = 0;
                using (var s = new GZipStream(desStream, CompressionMode.Decompress))
                {
                    using (var sReader = new StreamReader(s, Encoding.UTF8))
                    {
                        data = sReader.ReadToEnd();
                        sReader.Close();
                    }
                }
            }
            return data;
        }

        private static string GetStringFromStream(Stream stream)
        {
            string data = null;
            using (var desStream = new MemoryStream())
            {
                stream.CopyTo(desStream);
                desStream.Position = 0;
                using (var sReader = new StreamReader(desStream, Encoding.UTF8))
                {
                    data = sReader.ReadToEnd();
                    sReader.Close();
                }
            }
            return data;
        }
    }
}