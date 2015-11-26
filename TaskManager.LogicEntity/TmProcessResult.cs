namespace TaskManager.LogicEntity
{
    public class TmProcessResult<T>
    {
        public TmProcessError Error { get; set; }
        public T Data { get; set; }
        public bool HasError { get { return this.Error != null; } }

        public TmProcessResult() { }
        public TmProcessResult(T data)
            : this(data, null)
        {
        }
        public TmProcessResult(TmProcessError error)
            : this(default(T), error)
        {
        }
        public TmProcessResult(T data, TmProcessError error)
        {
            this.Data = data;
            this.Error = error;
        }
    }
}
