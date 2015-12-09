using TaskManager.DB;

namespace TaskManager.DBEntity.UB
{
    [TableName("T_UB_USER_LOG")]
    [PrimaryKey("UUL_Id", autoIncrement = false)]
    public class T_UB_USER_LOG : BaseUbDbEneity
    {
        [Column("UUL_Id")]
        public string Id { get; set; }
        [Column("OU_Id")]
        public string UserId { get; set; }
    }
}
