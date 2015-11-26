using TaskManager.DBEntity.DEV;
using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Repository.Converter.Dev
{
    internal static class NodeHeartBeatConverter
    {
        public static T_DEV_NODE_HEART_BEAT ToT(this NodeHeartBeat l)
        {
            return ModelConvertorHelper.ConvertModel<T_DEV_NODE_HEART_BEAT, NodeHeartBeat>(l);
        }

        public static NodeHeartBeat FromT(this T_DEV_NODE_HEART_BEAT t)
        {
            return ModelConvertorHelper.ConvertModel<NodeHeartBeat, T_DEV_NODE_HEART_BEAT>(t);
        }
    }
}
