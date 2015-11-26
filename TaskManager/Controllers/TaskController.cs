using System.Linq;
using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Models.Ta;
using TaskManager.Service.Interfaces.Dev;
using TaskManager.Service.Interfaces.Ta;

namespace TaskManager.Controllers
{
    public class TaskController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "TASK";
        }

        #region Views

        [HttpGet]
        public ActionResult Index(int pageIndex = 1)
        {
            var service = base.ResolveService<ITaskService>();
            var result = service.GetByCondition(pageIndex, DefaultPageSize);

            return base.ReturnView(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var service = base.ResolveService<INodeService>();
            var result = service.GetAllNode();

            if (result.HasError)
            {
                ViewBag.Error = result.Error.Message;
            }
            else
            {
                var model = new CreateTaskModel()
                {
                    Nodes = result.Data.Select(a => new SelectListItem()
                    {
                        Text = a.Name,
                        Value = a.Id
                    }).ToList()
                };
                return base.ReturnView(model);
            }

            return base.ReturnView();
        }

        #endregion

        #region Post

        [HttpPost]
        public ActionResult Create(CreateTaskModel model)
        {
            if (ModelState.IsValid)
            {
                var service = base.ResolveService<ITaskService>();
                var result = service.CreateTask(model.Name, model.NodeId, model.Cron, model.ClassName, model.MethodName,
                    model.Remark, model.File.FileName, model.File.InputStream);
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