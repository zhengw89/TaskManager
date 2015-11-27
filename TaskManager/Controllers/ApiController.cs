using System.Web.Mvc;
using TaskManager.Controllers.Base;
using TaskManager.Service.Interfaces.Dev;

namespace TaskManager.Controllers
{
    public partial class ApiController : ApiBaseController
    {
        [HttpPost]
        public ActionResult HeartBeat(string nodeId)
        {
            var service = base.ResolveService<INodeService>();
            return JsonResult(service.CreateNodeHeartBeat(nodeId));
        }
    }
}