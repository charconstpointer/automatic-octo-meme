using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Moderato.Application.ViewModels
{
    public class RepositoryViewModel
    {
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonProperty(PropertyName = "letters")]
        public Dictionary<char, int> Letters { get; set; }
        [JsonProperty(PropertyName = "avgStargazers")]
        public double Stars { get; set; }
        [JsonProperty(PropertyName = "avgWatchers")]
        public double Watchers { get; set; }
        [JsonProperty(PropertyName = "avgForks")]
        public double Forks { get; set; }
        [JsonProperty(PropertyName = "avgSize")]
        public double Size { get; set; }
    }
}