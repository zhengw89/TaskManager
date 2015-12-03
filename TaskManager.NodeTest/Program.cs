using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Node.SystemMonitor;
using TaskManager.Node.SystemRuntime;
using TaskManager.Utils;

namespace TaskManager.NodeTest
{
    class Program
    {
        private const string NodeId = "30d9d626-0047-49d4-ac0b-a8aeb3713b2b",
            Host = "http://localhost/",
            TaskFileSaveRootPath = @"C:\Users\zhengw\Documents\TaskManager\TaskManager.NodeTest\bin\Debug\TaskFiles\";

        static void Main(string[] args)
        {
            DirectoryAndFileHelper.CreateDirectoryIfNotExists(TaskFileSaveRootPath);

            //MonitorManager.Insatnce.StartMonitoring(Host, NodeId, true);

            TaskPoolManager.Instance.Init(Host, NodeId, TaskFileSaveRootPath);
        }
    }
}
