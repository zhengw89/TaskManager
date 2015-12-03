using TaskManager.DB;

namespace TaskManager.DBEntity.TA
{
    [TableName("T_TASK")]
    [PrimaryKey("TA_Id", autoIncrement = false)]
    public class T_TASK : DbBaseEntity
    {
        [Column("TA_Id")]
        public string Id { get; set; }
        [Column("TA_Name")]
        public string Name { get; set; }
        [Column("NODE_Id")]
        public string NodeId { get; set; }
        [Column("TA_CRON")]
        public string Cron { get; set; }
        [Column("TA_DllName")]
        public string DllName { get; set; }
        [Column("TA_FullClassName")]
        public string ClassName { get; set; }
        [Column("TA_Remark")]
        public string Remark { get; set; }
    }
}
