﻿namespace TaskManager.LogicEntity.Entities.Ta
{
    public class Task : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NodeId { get; set; }
        public string Cron { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Remark { get; set; }
    }
}
