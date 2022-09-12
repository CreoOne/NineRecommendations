using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public class ExternalUrls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; } = string.Empty;
    }
}
