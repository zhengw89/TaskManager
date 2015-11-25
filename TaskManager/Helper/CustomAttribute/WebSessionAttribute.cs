using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TaskManager.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class WebSessionAttribute : AuthorizeAttribute
    {
        private readonly bool _skipSession;
        public WebSessionAttribute(bool skipSession = false)
        {
            _skipSession = skipSession;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_skipSession)
            {
                return true;
            }

            if (String.IsNullOrEmpty(ContextHelper.GetCurrentUserId()))
            {
                return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var redirectTargetDictionary = new RouteValueDictionary
                    {
                        {"action", "Index"},
                        {"controller", "Login"}
                    };
            filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
        }
    }
}