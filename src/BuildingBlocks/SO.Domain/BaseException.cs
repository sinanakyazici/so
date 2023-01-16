namespace SO.Domain
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public BaseException(string message, int statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}