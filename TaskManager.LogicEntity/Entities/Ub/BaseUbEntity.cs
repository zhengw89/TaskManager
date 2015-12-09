using System;
using TaskManager.LogicEntity.Enums.Ub;

namespace TaskManager.LogicEntity.Entities.Ub
{
    public abstract class BaseUbEntity : BaseEntity
    {
        public string OperatorId { get; set; }
        public DataOperateType OperateType { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
