using System.Web.Mvc;

namespace TaskManager.Helper.CustomResult
{
    public class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}