using TaskManager.ApiSdk.Sdk;

namespace TaskManager.ApiSdk.Factory
{
    public static class SdkFactory
    {
        public static ITmSdk CreateSdk(SdkConfig config)
        {
            return new TmSdk(config);
        }
    }
}
