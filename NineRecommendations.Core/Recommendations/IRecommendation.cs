using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Core.Recommendations
{
    public interface IRecommendation
    {
        Guid Id { get; }
        IEnumerable<Track> Recommendations { get; }
        RecommendationStatus Status { get; }
    }
}