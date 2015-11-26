using TaskManager.DB;

namespace TaskManager.DBEntity.DEV
{
    [TableName("T_DEV_NODE")]
    [PrimaryKey("NODE_Id", autoIncrement = false)]
    public class T_DEV_NODE : DbBaseEntity
    {
        [Column("NODE_Id")]
        public string Id { get; set; }
        [Column("NODE_Name")]
        public string Name { get; set; }
        [Column("NODE_IP")]
        public string IP { get; set; }
        [Column("NODE_Port")]
        public int Port { get; set; }
        [Column("NODE_Remark")]
        public string Remark { get; set; }
    }
}
