using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.Questionnaries.SingleChoice.Time;

namespace NineRecommendations.Spotify.Recommendations
{
    public class RecommendationBuilder : IRecommendationBuilder
    {
        private ISpotifyApi SpotifyApi { get; }

        public RecommendationBuilder(ISpotifyApi spotifyApi)
        {
            SpotifyApi = spotifyApi;
        }

        public IRecommendation? BuildRecommendation(IAnswer answer, IQuestionnaire questionnaire)
        {
            if(answer.Id != new Timeless().Id)
                return null;

            return new Recommendation(Guid.NewGuid(), questionnaire, SpotifyApi);
        }
    }
}
