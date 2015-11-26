using TaskManager.DBEntity.DEV;
using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Repository.Converter.Dev
{
    internal static class NodeConverter
    {
        public static T_DEV_NODE ToT(this Node l)
        {
            return ModelConvertorHelper.ConvertModel<T_DEV_NODE, Node>(l);
        }

        public static Node FromT(this T_DEV_NODE t)
        {
            return ModelConvertorHelper.ConvertModel<Node, T_DEV_NODE>(t);
        }
    }
}
