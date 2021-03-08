using System;
using System.Net;

namespace Insight.Application.Common.Exceptions
{
    public class PostcodesEmptyResponseException : Exception
    {
        public PostcodesEmptyResponseException(HttpStatusCode statusCode)
            : base(string.Format("No response was provided; HTTP status: {0}", (int)statusCode)) { }
    }
}
