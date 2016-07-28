using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Interfaces.Ub;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.UserOperator.Deleter
{
    internal class UserDeleterDependent : TmBaseDependentProvider
    {
        public UserDeleterDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
            base.RegistRepository<IUserLogRepository>();
        }
    }

    internal class UserDeleter : TmOperateProcess
    {
        private readonly string _userId;

        private readonly IUserRepository _userRepository;
        private readonly IUserLogRepository _userLogRepository;

        public UserDeleter(ITmProcessConfig config, string userId)
            : base(config)
        {
            this._userId = userId;

            this._userRepository = base.ResolveDependency<IUserRepository>();
            this._userLogRepository = base.ResolveDependency<IUserLogRepository>();
        }

        protected override bool PreCheckProcessDataLegal()
        {
            if (string.IsNullOrEmpty(this._userId))
            {
                base.CacheProcessError("用户ID不可为空");
                return false;
            }

            if (!this._userRepository.Exists(this._userId))
            {
                base.DirectSuccessProcess();
                return true;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            if (!this._userRepository.Delete(this._userId))
            {
                base.CacheProcessError("删除用户失败");
                return false;
            }

            return true;
        }
    }
}
