using System.Web.Mvc;
using TaskManager.Controllers.Base;

namespace TaskManager.Controllers
{
    public class UserController:WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "USER";
        }

        #region Views

        public ActionResult Index(int pageIndex = 1)
        {
            return base.ReturnView();
        }

        #endregion
    }
}