using System;
using System.Web.Mvc;

namespace TaskManager.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    internal class ApiExceptionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //filterContext.ExceptionHandled = true;
            //var exception = filterContext.Exception;
            //string message = string.Format("\n====== {0}Controller=>{1}Action ======\n记录时间：{2}\n错误类型{3}详细信息：{4}\n=============",
            //    filterContext.RouteData.GetRequiredString("controller"),
            //    filterContext.RouteData.GetRequiredString("action"),
            //    DateTime.Now,
            //    exception.GetType().Name,
            //    exception.Message + "\n引发异常的方法：" +
            //    exception.TargetSite + "\n引发异常的对象：" +
            //    exception.Source);

            //var logMessage = new LogMessage(message, MessageType.Error);
            //Logger.LogInstance.Write(logMessage);

            //var error = new ApiJsonResult<object>() { Error = new ApiJsonError(-1000, "server unknow error:" + filterContext.Exception.Message) };
            //var platform = filterContext.HttpContext.Request.Headers[ConstValue.Platform];
            //if (string.IsNullOrEmpty(platform))
            //{
            //    platform = ConstValue.Platform_iOS;
            //}
            //if (WebConfigHelper.Instance.ApiConfig.NeedEncryptContent && !String.IsNullOrEmpty(platform))
            //{
            //    filterContext.Result = new ContentResult() { Content = AesHelper.AesEncrypt(platform, JsonNet.SerializeToEntity(error)) };
            //}
            //else
            //{
            //    filterContext.Result = new ContentResult() { Content = JsonNet.SerializeToEntity(error), ContentType = "application/json; charset=utf-8;" };
            //}
        }
    }
}