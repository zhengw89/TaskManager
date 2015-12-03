using System.Collections.Generic;
using System.IO;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.ApiSdk.Sdk
{
    public interface ITmSdk
    {
        TmSdkResult<bool> HeartBeat(string nodeId);

        TmSdkResult<List<Task>> GetTasks(string nodeId);

        TmSdkResult<Stream> DownloadTaskFile(string taskId);

        TmSdkResult<string> StartTask(string nodeId, string taskId);

        TmSdkResult<bool> CompleteTask(string jobId, bool success, string result);
    }
}
