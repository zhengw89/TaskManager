using System;

namespace TaskManager.AgentService.SystemMonitor
{
    public interface IMonitor : IDisposable
    {
        void StartMonitor();

        void StopMonitor();
    }
}
