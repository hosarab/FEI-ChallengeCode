namespace Insight.Application.Common.Exceptions
{
    public class PostcodeException : NotFoundException
    {
        public PostcodeException(int status, string error, string message) : base(message)
        {
            Error = error;
            Status = status;
        }
        public int Status { get; set; }
        public string Error { get; set; }

    }
}