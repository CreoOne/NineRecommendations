using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Core.Recommendations
{
    public interface IRecommendation
    {
        Guid Id { get; }
        string Name { get; }
        IEnumerable<Track> Recommendations { get; }
        DateTime Created { get; }
        RecommendationStatus Status { get; }
    }
}