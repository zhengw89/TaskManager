using System.Web.Mvc;
using TaskManager.Helper;
using TaskManager.Helper.CustomAttribute;

namespace TaskManager.Controllers.Base
{
    [ApiAuthorize]
    public class ApiBaseController : PrivateBaseController
    {
        protected ContentResult JsonResult(object obj)
        {
            return new ContentResult()
            {
                Content = JsonNet.SerializeToString(obj),
                ContentType = JsonContentType
            };
        }
    }
}