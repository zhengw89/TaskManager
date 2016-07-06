using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.LogicEntity.Entities.Ub;
using TaskManager.LogicEntity.Enums.Ub;
using TaskManager.Repository.Interfaces.Org;
using TaskManager.Repository.Interfaces.Ub;
using TaskManager.Service.Core;

namespace TaskManager.Service.Service.Org.UserOperator.Updater
{
    internal class UserUpdaterDependent : TmBaseDependentProvider
    {
        public UserUpdaterDependent(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override void RegistDefault()
        {
            base.RegistRepository<IUserRepository>();
            base.RegistRepository<IUserLogRepository>();
        }
    }

    internal class UserUpdater : TmOperateProcess
    {
        private readonly string _userId, _userName;

        private readonly IUserRepository _userRepository;
        private readonly IUserLogRepository _userLogRepository;

        public UserUpdater(ITmProcessConfig config, string userId, string userName)
            : base(config)
        {
            this._userId = userId;
            this._userName = userName;

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

            if (!this._userRepository.Exists(this._userId))
            {
                base.CacheProcessError("不存在指定用户");
                return false;
            }

            return true;
        }

        protected override bool ProcessMainData()
        {
            var user = this._userRepository.Get(this._userId);
            if (user == null)
            {
                base.CacheProcessError("不存在指定用户");
                return false;
            }

            user.Name = this._userName;
            user.UpdateTime = DateTime.Now;

            if (!this._userRepository.Update(user))
            {
                base.CacheProcessError("用户更新失败");
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
                OperateType = DataOperateType.Update,
                OperatorId = base.UserId,
                UpdateTime = DateTime.Now,
                UserId = this._userId
            }))
            {
                base.CacheProcessError("创建用户信息操作日志错误");
                return false;
            }

            return base.RecordLogInfo();
        }
    }
}
