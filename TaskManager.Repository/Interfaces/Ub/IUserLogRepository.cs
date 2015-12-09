using TaskManager.LogicEntity.Entities.Ub;

namespace TaskManager.Repository.Interfaces.Ub
{
    public interface IUserLogRepository
    {
        bool Create(UserLog userLog);
    }
}
