using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Service.Interfaces.Ta;

namespace TaskManager.Controllers
{
    public class TaskJobController : WebBaseController
    {
        protected override string GetPageCategoryName()
        {
            return "TASKJOB";
        }

        #region Views

        [HttpGet]
        public ActionResult Index(int pageIndex = 1)
        {
            var service = base.ResolveService<ITaskService>();
            var queryResult = service.GetTaskJobByCondition(pageIndex, DefaultPageSize);
            return base.ReturnView(queryResult);
        }

        #endregion
    }
}