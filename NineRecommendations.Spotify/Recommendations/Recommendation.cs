using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;
using NineRecommendations.Spotify.Extensions;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.External.Options;
using NineRecommendations.Spotify.Questionnaries;

namespace NineRecommendations.Spotify.Recommendations
{
    public class Recommendation : IRecommendation
    {
        public Guid Id { get; }
        public string Name { get; }
        public RecommendationStatus Status { get; private set; } = RecommendationStatus.Processing;
        public DateTime Created { get; } = DateTime.UtcNow;
        public IEnumerable<Track> Recommendations { get; private set; } = Enumerable.Empty<Track>();

        public Recommendation(Guid id, string name, IQuestionnaire questionnaire, ISpotifyApi spotifyApi)
        {
            Id = id;
            Name = name;
            Task.Factory.StartNew(async () => await ProcessAsync(questionnaire, spotifyApi).ConfigureAwait(false));
        }

        private async Task ProcessAsync(IQuestionnaire questionnaire, ISpotifyApi spotifyApi)
        {
            try
            {
                var searchOptions = new SearchOptions
                {
                    Genre = SelectGenre(questionnaire),
                    Year = SelectTime(questionnaire)
                };

                // needs more than one search to give more diverse recommendations
                var searchResult = await spotifyApi.CallSearchAsync(searchOptions);

                if (searchResult == null || searchResult.Tracks.Items.Length == 0)
                {
                    Status = RecommendationStatus.Empty;
                    return;
                }

                // needs shuffling from wider array of tracks to make recommendations more diverse
                Recommendations = searchResult.Tracks.Items.ToTracks().Take(9);
                Status = RecommendationStatus.Ready;
            }

            catch
            {
                Status = RecommendationStatus.Error;
            }
        }

        private static string? SelectGenre(IQuestionnaire questionnaire)
        {
            var answerId = GetQuestionAnswerId(questionnaire, Questions.Activity);

            if (answerId == null)
                return null;

            if (answerId == Answers.Sleep.Id)
                return "ambient";

            if (answerId == Answers.Explore.Id)
                return "chill";

            if (answerId == Answers.Chopper.Id)
                return "rock-n-roll";

            if (answerId == Answers.Sunbathe.Id)
                return "deep-house";

            return null;
        }

        private static Range SelectTime(IQuestionnaire questionnaire)
        {
            var answerId = GetQuestionAnswerId(questionnaire, Questions.Time);

            if (answerId == Answers.OldSchool.Id)
                return new Range(1970, 1990);

            return new Range(1900, DateTime.UtcNow.Year);
        }

        private static Guid? GetQuestionAnswerId(IQuestionnaire questionnaire, IQuestion question)
        {
            questionnaire.GetQuestionAnswerPairs().TryGetValue(question.Id, out var answer);
            return answer;
        }
    }
}
