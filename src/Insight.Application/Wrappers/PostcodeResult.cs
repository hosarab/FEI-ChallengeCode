using System;

namespace Insight.Application.Wrappers
{
    [Serializable]
    public class PostcodeResult
    {

        public string Postcode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}