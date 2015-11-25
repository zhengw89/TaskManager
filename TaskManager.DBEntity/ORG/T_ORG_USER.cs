using TaskManager.DB;

namespace TaskManager.DBEntity.ORG
{
    [TableName("T_ORG_USER")]
    [PrimaryKey("OU_Id", autoIncrement = false)]
    public class T_ORG_USER : DbBaseEntity
    {
        [Column("OU_Id")]
        public string Id { get; set; }
        [Column("OU_Name")]
        public string Name { get; set; }
        [Column("OU_Password")]
        public string Password { get; set; }
    }
}
