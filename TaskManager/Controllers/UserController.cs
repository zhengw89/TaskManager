using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Models.Org;
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

        [HttpGet]
        public ActionResult Index(int pageIndex = 1)
        {
            var serivce = base.ResolveService<IUserService>();
            var result = serivce.GetByCondition(pageIndex, DefaultPageSize);
            return base.ReturnView(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return base.ReturnView();
        }

        #endregion

        #region Post

        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return base.ReturnView("Create", model);
            }

            var service = base.ResolveService<IUserService>();
            var result = service.CreateUser(model.UserId, model.UserName, model.Password);
            if (result.Data)
            {
                return base.RedirectToRoute("Users");
            }
            else
            {
                ViewBag.Error = result.Error.Message;
                return base.ReturnView("Create", model);
            }
        }

        #endregion
    }
}