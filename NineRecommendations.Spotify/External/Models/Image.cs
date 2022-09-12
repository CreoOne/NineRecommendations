using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public class Image
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}
