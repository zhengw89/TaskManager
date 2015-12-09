using TaskManager.DBEntity.UB;
using TaskManager.LogicEntity.Entities.Ub;

namespace TaskManager.Repository.Converter.Ub
{
    internal static class UserLogConverter
    {
        public static T_UB_USER_LOG ToT(this UserLog l)
        {
            return ModelConvertorHelper.ConvertModel<T_UB_USER_LOG, UserLog>(l);
        }

        public static UserLog FromT(this T_UB_USER_LOG t)
        {
            return ModelConvertorHelper.ConvertModel<UserLog, T_UB_USER_LOG>(t);
        }
    }
}
