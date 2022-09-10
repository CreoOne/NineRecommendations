using NineRecommendations.Core.Recommendations;

namespace NineRecommendations.Core.Persistence
{
    public interface IRecommendationRepository
    {
        Task EnqueueNewRecommendationJob(IRecommendation recommendation);
        Task<IEnumerable<IRecommendation>> ListAllRecommendations();
    }
}
