using System;

namespace TaskManager.LogicEntity.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
