using System;
using TaskManager.LogicEntity.Enums.Ta;

namespace TaskManager.LogicEntity.Entities.Ta
{
    public class TaskJob : BaseEntity
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string NodeId { get; set; }
        public DateTime ExecuteTime { get; set; }
        public TaskJobStatus Status { get; set; }
        public string ResultMessage { get; set; }
    }
}
