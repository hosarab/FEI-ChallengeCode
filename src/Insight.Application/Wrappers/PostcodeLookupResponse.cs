using System;

namespace Insight.Application.Wrappers
{
    [Serializable]
    public class PostcodeLookupResponse
    {
        public int Status { get; set; }

        public PostcodeLookupResult Result { get; set; }
    }
}