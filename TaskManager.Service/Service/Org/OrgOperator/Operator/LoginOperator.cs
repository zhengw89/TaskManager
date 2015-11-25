using TaskManager.DB;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.OrgOperator.Operator
{
    internal class LoginOperatorDependent : TmBaseDependentProvider
    {
        public LoginOperatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
        }
    }

    internal class LoginOperator : TmOperateProcess
    {
        private readonly string _userId, _password;
        private readonly IUserRepository _userRepository;

        public LoginOperator(ITmProcessConfig config, string userId, string password)
            : base(config)
        {
            this._userId = userId;
            this._password = password;

            this._userRepository = base.ResolveDependency<IUserRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._userId) || string.IsNullOrEmpty(this._password))
            {
                base.CacheProcessError("用户名、密码为空");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            var user = this._userRepository.Get(this._userId);
            if (user == null)
            {
                base.CacheProcessError("用户不存在");
                return false;
            }

            if (!user.Password.Equals(this._password))
            {
                base.CacheProcessError("身份信息验证失败");
                return false;
            }

            return true;
        }
    }
}
