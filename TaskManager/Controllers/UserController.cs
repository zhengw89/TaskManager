using System;
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

        [HttpGet]
        public ActionResult Edit(string userId)
        {
            var service = base.ResolveService<IUserService>();
            var userResult = service.GetById(userId);
            if (userResult.HasError)
            {
                return base.RedirectToRoute("Users");
            }

            return base.ReturnView(new EditUserModel()
            {
                UserId = userResult.Data.Id,
                UserName = userResult.Data.Name
            });
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

        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return base.ReturnView("Edit", model);
            }

            var service = base.ResolveService<IUserService>();
            var result = service.EditUser(model.UserId, model.UserName);
            if (result.Data)
            {
                return base.RedirectToRoute("Users");
            }
            else
            {
                ViewBag.Error = result.Error.Message;
                return base.ReturnView("Edit", model);
            }
        }

        [HttpPost]
        public ActionResult Delete(string userId)
        {
            var service = base.ResolveService<IUserService>();
            service.DeleteUser(userId);

            return base.RedirectToRoute("Users");
        }

        #endregion
    }
}