using CommonProcess;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.UserOperator.Queryer
{
    internal class UserByConditionQueryerDependent : TmBaseDependentProvider
    {
        public UserByConditionQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
        }
    }

    internal class UserByConditionQueryer : TmQueryProcess<PagedList<User>>
    {
        private readonly int _pageIndex, _pageSize;

        private readonly IUserRepository _userRepository;

        public UserByConditionQueryer(ITmProcessConfig config, int pageIndex, int pageSize)
            : base(config)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;

            this._userRepository = base.ResolveDependency<IUserRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (this._pageIndex < 0 || this._pageSize < 0)
            {
                base.CacheProcessError("分页参数错误");
                return false;
            }

            return true;
        }

        protected override PagedList<User> Query()
        {
            return this._userRepository.GetByCondition(this._pageIndex, this._pageSize);
        }
    }
}
