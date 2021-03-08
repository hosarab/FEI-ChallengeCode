using System.Text.Json.Serialization;

namespace Insight.Application.Wrappers
{
    public class PostcodeLookupResult
    {
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

    }
}
