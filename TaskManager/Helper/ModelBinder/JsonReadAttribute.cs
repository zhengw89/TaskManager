using System;
using System.Web.Mvc;

namespace TaskManager.Helper.ModelBinder
{
    public class JsonReadAttribute : CustomModelBinderAttribute
    {
        private readonly Type _type;

        public JsonReadAttribute(Type type)
        {
            this._type = type;
        }

        public override IModelBinder GetBinder()
        {
            return JsonModelBinderContainer.Instance.ResolveModelBinder(_type);
        }
    }
}