using NineRecommendations.Core.Recommendations;
using System.Collections.Concurrent;

namespace NineRecommendations.Core.Persistence
{
    public sealed class InMemoryRecommendationRepository : IRecommendationRepository
    {
        private ConcurrentDictionary<Guid, IRecommendation> Recommendations { get; } = new();

        public Task EnqueueNewRecommendationJobAsync(IRecommendation recommendation)
        {
            Recommendations.TryAdd(recommendation.Id, recommendation);
            return Task.CompletedTask;
        }

        public Task<IRecommendation?> GetRecommendationByIdAsync(Guid id)
        {
            Recommendations.TryGetValue(id, out IRecommendation? recommendation);
            return Task.FromResult(recommendation);
        }

        public Task<IEnumerable<IRecommendation>> ListAllRecommendationsAsync()
        {
            return Task.FromResult(Recommendations.Values.ToList().AsEnumerable()); // TODO not thread safe, can cause exception
        }
    }
}
