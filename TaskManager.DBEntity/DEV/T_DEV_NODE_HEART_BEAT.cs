using System;
using TaskManager.DB;

namespace TaskManager.DBEntity.DEV
{
    [TableName("T_DEV_NODE_HEART_BEAT")]
    [PrimaryKey("NODE_Id", autoIncrement = false)]
    public class T_DEV_NODE_HEART_BEAT : DbBaseEntity
    {
        [Column("NHB_Id")]
        public string Id { get; set; }
        [Column("NODE_Id")]
        public string NodeId { get; set; }
        [Column("NHB_Time")]
        public DateTime BeatTime { get; set; }
    }
}
