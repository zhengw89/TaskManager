using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Org;

namespace TaskManager.Service.Interfaces.Org
{
    public interface IUserService
    {
        TmProcessResult<bool> Login(string userId, string password);

        TmProcessResult<User> GetById(string userId);

        TmProcessResult<PagedList<User>> GetByCondition(int pageIndex, int pageSize);

        TmProcessResult<bool> CreateUser(string userId, string userName, string password);

        TmProcessResult<bool> EditUser(string userId, string userName);

        TmProcessResult<bool> DeleteUser(string userId);
    }
}
