using System.Text.Json.Serialization;

namespace GitHub.Models
{
    public class UserRepository
    {
        public string Name { get; set; }
        public int Size { get; set; }
        [JsonPropertyName("forks_count")] public int Forks { get; set; }
        [JsonPropertyName("stargazers_count")] public int Stargazers { get; set; }
        [JsonPropertyName("watchers_count")] public int Watchers { get; set; }
    }
}