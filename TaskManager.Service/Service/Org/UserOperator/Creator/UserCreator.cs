using System;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.LogicEntity.Entities.Ub;
using TaskManager.LogicEntity.Enums.Ub;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Interfaces.Ub;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.UserOperator.Creator
{
    internal class UserCreatorDependent : TmBaseDependentProvider
    {
        public UserCreatorDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
            base.RegistRepository<IUserLogRepository>();
        }
    }

    internal class UserCreator : TmOperateProcess
    {
        private readonly string _userId, _userName, _password;

        private readonly IUserRepository _userRepository;
        private readonly IUserLogRepository _userLogRepository;

        public UserCreator(ITmProcessConfig config, string userId, string userName, string password)
            : base(config)
        {
            this._userId = userId;
            this._userName = userName;
            this._password = password;

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
            if (string.IsNullOrEmpty(this._userName))
            {
                base.CacheProcessError("用户名称不可为空");
                return false;
            }
            if (string.IsNullOrEmpty(this._password))
            {
                base.CacheProcessError("用户密码不可为空");
                return false;
            }

            if (this._userRepository.Exists(this._userId))
            {
                base.CacheProcessError("当前用户ID已存在");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            if (!this._userRepository.Create(new User()
            {
                CreateTime = DateTime.Now,
                Id = this._userId,
                IsActive = true,
                Name = this._userName,
                Password = this._password,
                UpdateTime = DateTime.Now
            }))
            {
                base.CacheProcessError("创建用户信息失败");
                return false;
            }

            return true;
        }

        protected override bool RecordLogInfo()
        {
            if (!this._userLogRepository.Create(new UserLog()
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                OperateTime = DateTime.Now,
                OperateType = DataOperateType.Create,
                OperatorId = base.UserId,
                UpdateTime = DateTime.Now,
                UserId = this._userId
            }))
            {
                base.CacheProcessError("创建操作日志信息失败");
                return false;
            }

            return base.RecordLogInfo();
        }
    }
}
