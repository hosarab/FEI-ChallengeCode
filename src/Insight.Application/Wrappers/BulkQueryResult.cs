using System;

namespace Insight.Application.Wrappers
{
    [Serializable]
    public class BulkQueryResult<TQuery, TResult> where TResult : class
    {
        public TQuery Query { get; set; }
        public TResult Result { get; set; }
    }
}