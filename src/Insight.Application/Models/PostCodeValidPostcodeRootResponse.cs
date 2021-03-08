using System;
using System.Text.Json.Serialization;

namespace Insight.Application.Models
{
    [Serializable]
    public class PostCodeValidPostcodeRootResponse
    {
        public int Status { get; set; }
        
        public bool Result { get; set; }
        
        public string Error { get; set; }
    }
}