using System;
using TaskManager.DB;

namespace TaskManager.DBEntity
{
    [ExplicitColumns]
    public abstract class DbBaseEntity
    {
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
