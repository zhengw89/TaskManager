using System;
using System.Web.Mvc;
using TaskManager.Helper.CustomResult;
using TaskManager.Service.Interfaces.Ta;

namespace TaskManager.Controllers
{
    public partial class ApiController
    {
        [HttpGet]
        public ActionResult GetTasks(string nodeId)
        {
            var service = base.ResolveService<ITaskService>();
            return JsonResult(service.GetByNode(nodeId));
        }

        [HttpGet]
        public ActionResult GetTaskFile(string taskId)
        {
            var service = base.ResolveService<ITaskService>();
            var result = service.GetTaskFile(taskId);
            if (result.HasError)
            {
                return new Http404Result();
            }
            else
            {
                return File(result.Data, ZipContentType);
            }
        }

        [HttpPost]
        public ActionResult ExecuteTask(string nodeId, string taskId)
        {
            var service = base.ResolveService<ITaskService>();
            return JsonResult(service.StartTaskJob(nodeId, taskId));
        }

        [HttpPost]
        public ActionResult ExecuteTaskComplete(string jobId, bool success, string result)
        {
            var service = base.ResolveService<ITaskService>();
            return JsonResult(service.CompleteTaskJob(jobId, success, result));
        }
    }
}