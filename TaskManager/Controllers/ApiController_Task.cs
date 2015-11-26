using System;
using System.Web.Mvc;
using TaskManager.Service.Interfaces.Ta;

namespace TaskManager.Controllers
{
    public partial class ApiController
    {
        [HttpPost]
        public ActionResult GetTasks(string nodeId)
        {
            var service = base.ResolveService<ITaskService>();
            return Json(service.GetByNode(nodeId));
        }

        [HttpPost]
        public ActionResult ExecuteTask(string nodeId, string taskId)
        {
            var service = base.ResolveService<ITaskService>();
            return Json(service.StartTaskJob(nodeId, taskId));
        }

        [HttpPost]
        public ActionResult ExecuteTaskComplete(string jobId, bool success, string result)
        {
            throw new NotImplementedException();
        }
    }
}