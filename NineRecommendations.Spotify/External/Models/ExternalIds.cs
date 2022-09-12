using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public class ExternalIds
    {
        [JsonPropertyName("isrc")]
        public string Isrc { get; set; } = string.Empty;
    }
}
