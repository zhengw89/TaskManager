using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonProcess;
using TaskManager.LogicEntity.Entities;

namespace TaskManager.Service.Core
{
    internal abstract class TmPagedQueryProcess<T> : TmQueryProcess<PagedList<T>>
    {
        protected TmPagedQueryProcess(IDataProcessConfig config)
            : base(config)
        {
        }
    }
}
