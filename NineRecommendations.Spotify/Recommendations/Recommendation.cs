using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;

namespace NineRecommendations.Spotify.Recommendations
{
    public class Recommendation : IRecommendation
    {
        public Guid Id { get; }
        public RecommendationStatus Status { get; } = RecommendationStatus.Processing;
        public IEnumerable<Track> Recommendations { get; } = Enumerable.Empty<Track>();

        public Recommendation(Guid id, IQuestionnaire questionnaire)
        {
            Id = id;
            Task.Factory.StartNew(() => { });
        }
    }
}
