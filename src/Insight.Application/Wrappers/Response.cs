using System.Text.Json.Serialization;

namespace Insight.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Message = message;
        }
        public Response(string message, string error = null)
        {
            Message = message;
            Error = error;
        }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }
}
