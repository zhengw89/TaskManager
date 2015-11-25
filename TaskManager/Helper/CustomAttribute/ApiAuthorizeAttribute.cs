using System;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helper.CustomResult;

namespace TaskManager.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    internal class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _skipAuthotize;

        public ApiAuthorizeAttribute()
            : this(false)
        {
        }

        public ApiAuthorizeAttribute(bool skipAuthorize)
        {
            this._skipAuthotize = skipAuthorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_skipAuthotize)
            {
                return true;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new Http403Result();
        }
    }
}