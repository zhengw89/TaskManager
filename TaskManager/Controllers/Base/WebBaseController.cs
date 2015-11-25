using System.Web.Mvc;
using TaskManager.Helper.CustomAttribute;

namespace TaskManager.Controllers.Base
{
    [WebSession]
    public abstract class WebBaseController : PrivateBaseController
    {
        protected const int DefaultPageSize = 10;

        protected abstract string GetPageCategoryName();

        protected ViewResult ReturnView()
        {
            this.InitViewBagCommonData();
            return View();
        }

        protected ViewResult ReturnView(string viewName)
        {
            this.InitViewBagCommonData();
            return View(viewName);
        }

        protected ViewResult ReturnView(string viewName, object model)
        {
            this.InitViewBagCommonData();
            return View(viewName, model);
        }

        protected ViewResult ReturnView(object obj)
        {
            this.InitViewBagCommonData();
            return View(obj);
        }

        private void InitViewBagCommonData()
        {
            ViewBag.ActiveCategory = GetPageCategoryName();
        }
    }
}