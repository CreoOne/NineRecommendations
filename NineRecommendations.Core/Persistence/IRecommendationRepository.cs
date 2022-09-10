using NineRecommendations.Core.Recommendations;

namespace NineRecommendations.Core.Persistence
{
    public interface IRecommendationRepository
    {
        Task EnqueueNewRecommendationJobAsync(IRecommendation recommendation);
        Task<IEnumerable<IRecommendation>> ListAllRecommendationsAsync();
        Task<IRecommendation?> GetRecommendationByIdAsync(Guid id);
    }
}
