using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.LogicEntity.Entities.Org;

namespace TaskManager.Repository.Interfaces.Org
{
    public interface IUserRepository
    {
        bool Exists(string userId);

        bool Create(User user);

        User Get(string userId);
    }
}
