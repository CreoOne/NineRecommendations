using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.Questionnaries;

namespace NineRecommendations.Spotify.Recommendations
{
    public class RecommendationBuilder : IRecommendationBuilder
    {
        private ISpotifyApi SpotifyApi { get; }

        public RecommendationBuilder(ISpotifyApi spotifyApi)
        {
            SpotifyApi = spotifyApi;
        }

        private static readonly HashSet<Guid> LastAnswers = new()
        {
            Answers.Timeless.Id,
            Answers.OldSchool.Id
        };

        public IRecommendation? BuildRecommendation(IAnswer answer, IQuestionnaire questionnaire)
        {
            if(LastAnswers.Contains(answer.Id))
                return new Recommendation(Guid.NewGuid(), questionnaire, SpotifyApi);

            return null;
        }
    }
}
