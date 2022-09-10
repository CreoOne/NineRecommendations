using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Spotify.Recommendations;

namespace NineRecommendations.Spotify.Questionnaries.SingleChoice.Time
{
    public sealed class Timeless : ILastAnswer
    {
        public Guid Id => new("5661DA80-03E0-484B-B58E-51D39F0CE0E3");

        public string Content => "Timeless";

        public IRecommendation GetRecommendation(IQuestionnaire questionnaire) => new Recommendation(Guid.NewGuid(), questionnaire);
    }
}
