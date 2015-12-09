using TaskManager.DB;
using TaskManager.DBEntity.UB;
using TaskManager.LogicEntity.Entities.Ub;
using TaskManager.Repository.Converter.Ub;
using TaskManager.Repository.Interfaces.Ub;

namespace TaskManager.Repository.Repositories.Base.Ub
{
    internal abstract class BaseUserLogRepository : BaseRepository<UserLog, T_UB_USER_LOG>, IUserLogRepository
    {
        protected BaseUserLogRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override UserLog FromT(T_UB_USER_LOG t)
        {
            return t.FromT();
        }

        protected override T_UB_USER_LOG ToT(UserLog l)
        {
            return l.ToT();
        }

        public bool Create(UserLog userLog)
        {
            return base.Add(userLog.ToT());
        }
    }
}
