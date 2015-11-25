using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.Service.Interfaces.Org;
using TaskManager.Service.Service.Org.OrgOperator.Operator;
using TaskManager.Service.Service.Org.OrgOperator.Queryer;

namespace TaskManager.Service.Service.Org
{
    internal class UserService : BaseService, IUserService
    {
        public UserService(ServiceConfig config)
            : base(config)
        {
        }

        public TmProcessResult<bool> Login(string userId, string password)
        {
            return base.ExeProcess(db =>
            {
                var @operator = new LoginOperator(
                    base.ResloveProcessConfig<LoginOperator>(db),
                    userId, password);

                return base.ExeOperateProcess(@operator);
            });
        }

        public TmProcessResult<User> GetById(string userId)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new UserByIdQueryer(
                    base.ResloveProcessConfig<UserByIdQueryer>(db),
                    userId);

                return base.ExeQueryProcess(queryer);
            });
        }
    }
}
