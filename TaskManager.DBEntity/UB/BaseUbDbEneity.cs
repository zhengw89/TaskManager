using System;
using TaskManager.DB;

namespace TaskManager.DBEntity.UB
{
    public abstract class BaseUbDbEneity : DbBaseEntity
    {
        [Column("OperatorId")]
        public string OperatorId { get; set; }
        [Column("OperateType")]
        public int OperateType { get; set; }
        [Column("OperateTime")]
        public DateTime OperateTime { get; set; }
    }
}
