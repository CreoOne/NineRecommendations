using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Front.Models
{
    public sealed class RecommendationModel
    {
        public Guid Id { get; set; }
        public RecommendationStatus Status { get; set; }
        public DateTime Created { get; set; }
        public TrackModel[] Tracks { get; set; } = Array.Empty<TrackModel>();
    }
}
