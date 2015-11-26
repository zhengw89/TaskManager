using System;

namespace TaskManager.LogicEntity.Entities.Dev
{
    public class NodeHeartBeat : BaseEntity
    {
        public string Id { get; set; }

        public string NodeId { get; set; }

        public DateTime BeatTime { get; set; }
    }
}
