using System.Web.Mvc;
using TaskManager.Controllers.Base;

namespace TaskManager.Controllers
{
    public class HomeController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "HOME";
        }

        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return base.ReturnView();
        }
    }
}
