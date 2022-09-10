using NineRecommendations.Core.Recommendations;
using System.Collections.Concurrent;

namespace NineRecommendations.Core.Persistence
{
    public sealed class InMemoryRecommendationRepository : IRecommendationRepository
    {
        private ConcurrentBag<IRecommendation> Recommendations { get; } = new();

        public Task EnqueueNewRecommendationJob(IRecommendation recommendation)
        {
            Recommendations.Add(recommendation);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IRecommendation>> ListAllRecommendations()
        {
            return Task.FromResult(Recommendations.ToList().AsEnumerable());
        }
    }
}
