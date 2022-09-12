using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public sealed class Tracks
    {
        [JsonPropertyName("href")]
        public string Href { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public Item[] Items { get; set; } = Array.Empty<Item>();

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; } = string.Empty;

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        //[JsonPropertyName("previous")]
        //public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
