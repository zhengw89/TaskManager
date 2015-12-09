using System;
using Quartz;
using TaskCore;
using TaskManager.ApiSdk.Sdk;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.Node.SystemRuntime
{
    internal class TaskRuntimeInfo
    {
        public AppDomain AppDomain { get; set; }

        public Task TaskInfo { get; set; }

        public BaseTask ExeTask { get; set; }

        public ITmSdk TmSdk { get; set; }

        public IJobDetail JobDetail { get; set; }

        public ITrigger Trigger { get; set; }
    }
}
