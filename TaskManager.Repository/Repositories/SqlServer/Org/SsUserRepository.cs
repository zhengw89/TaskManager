using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DB;
using TaskManager.Repository.Repositories.Base.Org;

namespace TaskManager.Repository.Repositories.SqlServer.Org
{
    internal class SsUserRepository : BaseUserRepository
    {
        public SsUserRepository(ITaskManagerDb db)
            : base(db)
        {
        }
    }
}
