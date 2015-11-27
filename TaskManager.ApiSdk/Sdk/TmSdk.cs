using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;
using TaskManager.ApiSdk.Helper;
using TaskManager.LogicEntity;
using TaskManager.LogicEntity.Entities.Ta;

namespace TaskManager.ApiSdk.Sdk
{
    internal class TmSdk : ITmSdk
    {
        private readonly SdkConfig _config;

        public TmSdk(SdkConfig config)
        {
            this._config = config;
        }

        public TmSdkResult<bool> HeartBeat(string nodeId)
        {
            var request = new RestRequest("Api/HeartBeat", Method.POST);
            request.AddParameter("nodeId", nodeId);

            return this.ExecuteRequest<bool>(request);
        }

        public TmSdkResult<List<Task>> GetTasks(string nodeId)
        {
            var request = new RestRequest("Api/GetTasks?nodeId={nodeId}", Method.GET);
            request.AddUrlSegment("nodeId", nodeId);

            return this.ExecuteRequest<List<Task>>(request);
        }

        public TmSdkResult<Stream> DownloadTaskFile(string taskId)
        {
            var client = new RestClient(this._config.Host);

            var request = new RestRequest("Api/GetTaskFile?taskId={taskId}", Method.GET);
            request.AddUrlSegment("taskId", taskId);

            IRestResponse response = client.Execute(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new TmSdkResult<Stream>(new MemoryStream(response.RawBytes));
                default:
                    return new TmSdkResult<Stream>(string.Format("请求失败，HTTP状态为：{0}", response.StatusDescription));
            }
        }

        private TmSdkResult<T> ExecuteRequest<T>(RestRequest request)
        {
            var client = new RestClient(this._config.Host);

            IRestResponse response = client.Execute(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var requestResult = JsonNet.DeserializeToEntity<TmProcessResult<T>>(response.Content);
                    return new TmSdkResult<T>(requestResult);
                default:
                    return new TmSdkResult<T>(string.Format("请求失败，HTTP状态为：{0}", response.StatusDescription));
            }
        }
    }
}
