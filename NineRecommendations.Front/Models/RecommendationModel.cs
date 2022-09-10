using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Front.Models
{
    public sealed class RecommendationModel
    {
        public Guid Id { get; set; }
        public RecommendationStatus Status { get; set; }
        public DateTime Created { get; set; }
        public TrackModel[] Tracks { get; set; } = Array.Empty<TrackModel>();

        internal static RecommendationModel[] FromRecommendations(IEnumerable<IRecommendation> recommendations)
        {
            return recommendations.Select(recommendation => FromRecommendation(recommendation)).ToArray();
        }

        internal static RecommendationModel FromRecommendation(IRecommendation recommendation)
        {
            return new RecommendationModel
            {
                Id = recommendation.Id,
                Created = recommendation.Created,
                Status = recommendation.Status,
                Tracks = HandleTrackMarshalling(recommendation)
            };
        }

        private static TrackModel[] HandleTrackMarshalling(IRecommendation recommendation)
        {
            if (recommendation.Status == RecommendationStatus.Ready)
                return recommendation
                    .Recommendations
                    .Select(recommendation => TrackModel.FromTrack(recommendation))
                    .ToArray();

            return Array.Empty<TrackModel>();
        }
    }
}
