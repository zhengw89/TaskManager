using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Org;

namespace TaskManager.Repository.Repositories.MySql.Org
{
    internal class MsUserRepository : BaseUserRepository
    {
        public MsUserRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
