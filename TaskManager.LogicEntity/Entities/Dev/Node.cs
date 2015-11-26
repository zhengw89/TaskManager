namespace TaskManager.LogicEntity.Entities.Dev
{
    public class Node : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Remark { get; set; }
    }
}
