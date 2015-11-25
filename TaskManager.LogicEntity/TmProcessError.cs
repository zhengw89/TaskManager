namespace TaskManager.LogicEntity
{
    public class TmProcessError
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public TmProcessError() { }
        public TmProcessError(int errorCode, string errorMessage)
        {
            this.Code = errorCode;
            this.Message = errorMessage;
        }

        public override string ToString()
        {
            return string.Format("Code:{0},Message:{1}", Code, Message);
        }
    }
}
