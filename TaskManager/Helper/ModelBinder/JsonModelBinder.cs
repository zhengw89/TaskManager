using System.Web.Mvc;

namespace TaskManager.Helper.ModelBinder
{
    public class JsonModelBinder<T> : IModelBinder
        where T : class
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //将上下文对象中的字符串取出来
            string data = RequestHelper.GetRequestContent(controllerContext.HttpContext);
            return JsonNet.DeserializeToString<T>(data);
        }
    }
}