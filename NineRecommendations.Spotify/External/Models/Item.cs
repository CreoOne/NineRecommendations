using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public class Item
    {
        [JsonPropertyName("album")]
        public Album Album { get; set; } = new Album();

        [JsonPropertyName("artists")]
        public Artist[] Artists { get; set; } = Array.Empty<Artist>();

        [JsonPropertyName("available_markets")]
        public string[] Available_markets { get; set; } = Array.Empty<string>();

        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        public int DurationMs { get; set; }

        [JsonPropertyName("_explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds { get; set; } = new ExternalIds();

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; } = new ExternalUrls();

        [JsonPropertyName("href")]
        public string Href { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("preview_url")]
        public string PreviewUrl { get; set; } = string.Empty;

        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
