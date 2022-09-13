using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
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
            Answers.OldSchool.Id,
            Answers.Unpopular.Id,
            Answers.VeryFresh.Id
        };

        public IRecommendation? BuildRecommendation(IAnswer answer, IFinder finder, IQuestionnaire questionnaire)
        {
            if(LastAnswers.Contains(answer.Id))
                return new Recommendation(Guid.NewGuid(), ConstructName(finder, questionnaire), questionnaire, SpotifyApi);

            return null;
        }

        private static string ConstructName(IFinder finder, IQuestionnaire questionnaire)
        {
            var contents = questionnaire
                .GetQuestionAnswerPairs()
                .Values
                .Select(finder.FindAnswerById)
                .Where(answer => answer != null)
                .Select(answer => answer!.Content);

            return string.Join(", ", contents);
        }
    }
}
