using System;
using TaskManager.LogicEntity.Entities.Dev;

namespace TaskManager.Models.Dev
{
    public class NodeItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsActive { get; set; }

        public DateTime? LatestHeartBeat { get; set; }

        public NodeItemViewModel(Node node, NodeHeartBeat latestHeartBeat)
        {
            if (node != null)
            {
                this.Id = node.Id;
                this.Name = node.Name;
                this.IP = node.IP;
                this.Port = node.Port;
                this.Remark = node.Remark;
                this.CreateTime = node.CreateTime;
                this.UpdateTime = node.UpdateTime;
                this.IsActive = node.IsActive;
            }

            if (latestHeartBeat != null)
            {
                this.LatestHeartBeat = latestHeartBeat.BeatTime;
            }
        }
    }
}