using TaskManager.DBEntity.ORG;
using TaskManager.LogicEntity.Entities.Org;

namespace TaskManager.Repository.Converter.Org
{
    internal static class UserConverter
    {
        public static T_ORG_USER ToT(this User l)
        {
            return ModelConvertorHelper.ConvertModel<T_ORG_USER, User>(l);
        }

        public static User FromT(this T_ORG_USER t)
        {
            return ModelConvertorHelper.ConvertModel<User, T_ORG_USER>(t);
        }
    }
}
