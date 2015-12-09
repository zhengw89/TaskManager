using System;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.Service.Interfaces.Org;
using TaskManager.Service.Service.Org.UserOperator.Creator;
using TaskManager.Service.Service.Org.UserOperator.Operator;
using TaskManager.Service.Service.Org.UserOperator.Queryer;

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

        public TmProcessResult<PagedList<User>> GetByCondition(int pageIndex, int pageSize)
        {
            return base.ExeProcess(db =>
            {
                var queryer = new UserByConditionQueryer(
                    base.ResloveProcessConfig<UserByConditionQueryer>(db),
                    pageIndex, pageSize);
                return base.ExeQueryProcess(queryer);
            });
        }

        public TmProcessResult<bool> CreateUser(string userId, string userName, string password)
        {
            return base.ExeProcess(db =>
            {
                var creator = new UserCreator(
                    base.ResloveProcessConfig<UserCreator>(db),
                    userId, userName, password);

                return base.ExeOperateProcess(creator);
            });
        }
    }
}
