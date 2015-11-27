namespace TaskManager.ApiSdk.Sdk
{
    public class SdkConfig
    {
        private readonly string _host;

        public string Host { get { return this._host; } }

        public SdkConfig(string host)
        {
            this._host = host.EndsWith("/") ? host : host + "/";
        }
    }
}
