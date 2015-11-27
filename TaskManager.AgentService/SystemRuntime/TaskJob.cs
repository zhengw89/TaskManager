using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace TaskManager.AgentService.SystemRuntime
{
    internal class TaskJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
