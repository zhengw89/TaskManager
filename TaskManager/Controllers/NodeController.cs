using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Models.Dev;
using TaskManager.Service.Interfaces.Dev;

namespace TaskManager.Controllers
{
    public class NodeController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "NODE";
        }

        #region Views

        [HttpGet]
        public ActionResult Index(int pageIndex = 1)
        {
            var service = base.ResolveService<INodeService>();
            var result = service.GetByCondition(pageIndex, DefaultPageSize);
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
        public ActionResult Create(CreateNodeModel model)
        {
            if (ModelState.IsValid)
            {
                var service = base.ResolveService<INodeService>();
                var result = service.Create(model.Name, model.IP, model.Port, model.Remark);
                if (result.HasError)
                {
                    ViewBag.Error = result.Error.Message;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }



            return base.ReturnView("Create", model);
        }

        #endregion
    }
}