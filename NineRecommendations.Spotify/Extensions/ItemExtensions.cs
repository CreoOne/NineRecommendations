using NineRecommendations.Core.Recommendations.Primitives;
using NineRecommendations.Spotify.External.Models;

namespace NineRecommendations.Spotify.Extensions
{
    public static class ItemExtensions
    {
        public static IEnumerable<Track> ToTracks(this Item[] items) => items
            .Select(ToTrack);

        public static Track ToTrack(this Item item)
        {
            var artists = string.Join(", ", item.Artists.Select(artist => artist.Name));
            var duration = TimeSpan.FromMilliseconds(item.DurationMs);
            var uri = new Uri(item.ExternalUrls.Spotify);

            return new Track(item.Name, artists, duration, uri);
        }
    }
}
