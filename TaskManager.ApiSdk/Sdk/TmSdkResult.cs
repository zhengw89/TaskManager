using TaskManager.LogicEntity;

namespace TaskManager.ApiSdk.Sdk
{
    public class TmSdkResult<T>
    {
        private readonly string _errorMessage;
        private readonly T _data;

        public string ErrorMessage { get { return this._errorMessage; } }
        public T Data { get { return this._data; } }
        public bool HasError { get { return !string.IsNullOrEmpty(this._errorMessage); } }

        public TmSdkResult(T data) : this(data, null) { }

        public TmSdkResult(string errorMessage) : this(default(T), errorMessage) { }

        public TmSdkResult(T data, string errorMesage)
        {
            this._data = data;
            this._errorMessage = errorMesage;
        }

        public TmSdkResult(TmProcessResult<T> result)
        {
            this._data = result.Data;
            this._errorMessage = result.HasError ? result.Error.Message : null;
        }
    }
}
