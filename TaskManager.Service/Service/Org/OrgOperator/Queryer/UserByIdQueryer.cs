using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.OrgOperator.Queryer
{
    internal class UserByIdQueryerDependent : TmBaseDependentProvider
    {
        public UserByIdQueryerDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
        }
    }

    internal class UserByIdQueryer : TmQueryProcess<User>
    {
        private readonly string _userId;

        private readonly IUserRepository _userRepository;

        public UserByIdQueryer(ITmProcessConfig config, string userId)
            : base(config)
        {
            this._userId = userId;
            this._userRepository = base.ResolveDependency<IUserRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._userId))
            {
                base.CacheProcessError("用户ID为空");
                return false;
            }
            if (!this._userRepository.Exists(this._userId))
            {
                base.CacheProcessError("用户不存在");
                return false;
            }

            return true;
        }

        protected override User Query()
        {
            return this._userRepository.Get(this._userId);
        }
    }
}
