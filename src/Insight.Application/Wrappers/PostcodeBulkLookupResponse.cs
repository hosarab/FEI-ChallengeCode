using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Insight.Application.Wrappers
{
    [Serializable]
    public class PostcodeBulkLookupResponse
    {
        public int Status { get; set; }

        public IList<QueryResult> Result { get; set; }
    }
}