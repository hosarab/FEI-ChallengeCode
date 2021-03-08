using System;
using Insight.Application.Models;
using Newtonsoft.Json;

namespace Insight.Application.PostCodesFeatures.Queries
{
    [Serializable]
    public class PostcodeDetailsViewModel
    {
        public string Postcode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CoordinatesModel Coordinates { get; set; }
    }
}
