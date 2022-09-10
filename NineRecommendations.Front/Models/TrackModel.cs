using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Front.Models
{
    public class TrackModel
    {
        public string Name { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public Uri? Uri { get; set; }

        internal static TrackModel FromTrack(Track track)
        {
            return new TrackModel
            {
                Name = track.Name,
                Authors = track.Authors,
                Duration = track.Duration,
                Uri = track.Uri
            };
        }
    }
}
