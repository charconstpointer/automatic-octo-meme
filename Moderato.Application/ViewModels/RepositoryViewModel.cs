using System;
using System.Text.Json.Serialization;

namespace Moderato.Application.ViewModels
{
    public class RepositoryViewModel
    {
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonPropertyName("letters")]
        public object Letters { get; set; }
        [JsonPropertyName("avgStargazers")]
        public double Stars { get; set; }
        [JsonPropertyName("avgWatchers")]
        public double Watchers { get; set; }
        [JsonPropertyName("avgForks")]
        public double Forks { get; set; }
        [JsonPropertyName("avgSize")]
        public double Size { get; set; }
    }
}