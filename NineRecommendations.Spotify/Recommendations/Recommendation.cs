using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Spotify.Recommendations
{
    public class Recommendation : IRecommendation
    {
        public Guid Id { get; }
        public RecommendationStatus Status { get; private set; } = RecommendationStatus.Processing;
        public IEnumerable<Track> Recommendations { get; private set; } = Enumerable.Empty<Track>();

        public Recommendation(Guid id, IQuestionnaire questionnaire)
        {
            Id = id;
            Task.Factory.StartNew(async () => await ProcessAsync(questionnaire));
        }

        private async Task ProcessAsync(IQuestionnaire questionnaire)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            Status = RecommendationStatus.Ready;
        }
    }
}
