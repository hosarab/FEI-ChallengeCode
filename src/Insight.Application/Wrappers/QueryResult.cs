using System;

namespace Insight.Application.Wrappers
{
    [Serializable]
    public class QueryResult
    {
        public string Query { get; set; }

        public PostcodeResult Result { get; set; }
    }
}