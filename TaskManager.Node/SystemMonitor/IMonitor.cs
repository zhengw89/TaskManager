using System;

namespace TaskManager.Node.SystemMonitor
{
    public interface IMonitor : IDisposable
    {
        void StartMonitor();

        void StopMonitor();
    }
}
