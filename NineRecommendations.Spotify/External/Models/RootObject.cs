using System.Text.Json.Serialization;

namespace NineRecommendations.Spotify.External.Models
{
    public class RootObject
    {
        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; } = new Tracks();
    }
}
