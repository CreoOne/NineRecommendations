using NineRecommendations.Core.Recommendations.Primitives;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Extensions
{
    public static class TrackModelExtensions
    {
        public static TrackModel[] ToViewModels(this IEnumerable<Track> tracks) => tracks
            .Select(ToViewModel)
            .ToArray();

        public static TrackModel ToViewModel(this Track track) => new()
        {
            Name = track.Name,
            Authors = track.Authors,
            Duration = track.Duration,
            Uri = track.Uri
        };
    }
}
