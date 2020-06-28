namespace NachoMatic.OrderHandling
{
    public class ProcessResult
    {
        #region Properties

        public string Message { get; }

        public bool Success { get; }

        #endregion Properties

        #region Constructors

        public ProcessResult(bool success, string message = "")
        {
            this.Success = success;
            this.Message = message;
        }

        #endregion Constructors
    }
}