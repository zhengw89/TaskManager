using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Service.Interfaces.Org;

namespace TaskManager.Controllers
{
    public class UserController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "USER";
        }

        #region Views

        public ActionResult Index(int pageIndex = 1)
        {
            var serivce = base.ResolveService<IUserService>();
            var result = serivce.GetByCondition(pageIndex, DefaultPageSize);
            return base.ReturnView(result);
        }

        #endregion
    }
}