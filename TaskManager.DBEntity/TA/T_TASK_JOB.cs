using System;
using TaskManager.DB;

namespace TaskManager.DBEntity.TA
{
    [TableName("T_TASK_JOB")]
    [PrimaryKey("TAJ_Id", autoIncrement = false)]
    public class T_TASK_JOB : DbBaseEntity
    {
        [Column("TAJ_Id")]
        public string Id { get; set; }
        [Column("TA_Id")]
        public string TaskId { get; set; }
        [Column("NODE_Id")]
        public string NodeId { get; set; }
        [Column("TAJ_ExecuteTime")]
        public DateTime ExecuteTime { get; set; }
        [Column("TAJ_Status")]
        public int Status { get; set; }
    }
}
