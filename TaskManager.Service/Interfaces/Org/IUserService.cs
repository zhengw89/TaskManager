﻿using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities.Org;

namespace TaskManager.Service.Interfaces.Org
{
    public interface IUserService
    {
        TmProcessResult<bool> Login(string userId, string password);

        TmProcessResult<User> GetById(string userId);
    }
}
