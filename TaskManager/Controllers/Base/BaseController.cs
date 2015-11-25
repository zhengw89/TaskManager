using System.Web.Mvc;
using TaskManager.Helper;
using TaskManager.Service;
using TaskManager.Service.Factory;

namespace TaskManager.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected const string JsonContentType = "application/json";

        protected BaseController()
        {

        }

        protected virtual ServiceConfig BuildServiceConfig()
        {
            return new ServiceConfig(
                WebConfigHelper.Instance.DbInfo.ConnectinString,
                WebConfigHelper.Instance.DbInfo.ConnectionProvider,
                WebConfigHelper.Instance.DbInfo.DbType,
                ContextHelper.GetCurrentUserId(),
                ContextHelper.GetCurrentUserName());
        }

        /// <summary>
        /// 解析Service方法
        /// </summary>
        /// <typeparam name="T">所要解析的Service接口</typeparam>
        /// <returns></returns>
        protected T ResolveService<T>()
        {
            return ServiceLocator.Container.Resolve<T>(BuildServiceConfig());
        }
    }
}