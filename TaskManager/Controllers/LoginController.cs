using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Controllers.Base;
using TaskManager.Helper;
using TaskManager.Models;
using TaskManager.Service.Interfaces.Org;

namespace TaskManager.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var service = base.ResolveService<IUserService>();
                var loginResult = service.Login(model.UserName, model.Password);
                if (loginResult.Error != null)
                {
                    ViewBag.Error = loginResult.Error.Message;
                }
                else
                {
                    var userResult = service.GetById(model.UserName);
                    if (userResult.Error != null)
                    {
                        ViewBag.Error = userResult.Error.Message;
                    }
                    else
                    {
                        ContextHelper.SetCurrentUserId(userResult.Data.Id);
                        ContextHelper.SetCurrentUserName(userResult.Data.Name);

                        FormsAuthentication.SetAuthCookie(userResult.Data.Id, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            ContextHelper.CleanCurrentSession();

            return RedirectToAction("Index");
        }
    }
}