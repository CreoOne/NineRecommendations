using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.External.Options;
using NineRecommendations.Spotify.Questionnaries.SingleChoice.Activity;
using NineRecommendations.Spotify.Questionnaries.SingleChoice.Time;

namespace NineRecommendations.Spotify.Recommendations
{
    public class Recommendation : IRecommendation
    {
        public Guid Id { get; }
        public RecommendationStatus Status { get; private set; } = RecommendationStatus.Processing;
        public DateTime Created { get; } = DateTime.UtcNow;
        public IEnumerable<Track> Recommendations { get; private set; } = Enumerable.Empty<Track>();

        public Recommendation(Guid id, IQuestionnaire questionnaire, ISpotifyApi spotifyApi)
        {
            Id = id;
            Task.Factory.StartNew(async () => await ProcessAsync(questionnaire, spotifyApi));
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

                if (!searchResult.TrackIds.Any())
                {
                    Status = RecommendationStatus.Empty;
                    return;
                }

                var tracksResult = await spotifyApi.CallTracksAsync(searchResult.TrackIds.ToArray());

                if (!tracksResult.Tracks.Any())
                {
                    Status = RecommendationStatus.Empty;
                    return;
                }

                // needs shuffling from wider array of tracks to make recommendations more diverse
                Recommendations = tracksResult.Tracks.Take(9);
                Status = RecommendationStatus.Ready;
            }

            catch(Exception exception)
            {
                Status = RecommendationStatus.Error;
            }
        }

        private static string? SelectGenre(IQuestionnaire questionnaire)
        {
            var answerId = GetQuestionAnswerId(questionnaire, new ActivityQuestion());

            if (answerId == null)
                return null;

            if (answerId == new SleepAnswer().Id)
                return "ambient";

            if (answerId == new ExploreAnswer().Id)
                return "drone";

            return null;
        }

        private static Range SelectTime(IQuestionnaire questionnaire)
        {
            var answerId = GetQuestionAnswerId(questionnaire, new TimeQuestion());

            // this needs more time answers

            return new Range(1900, DateTime.UtcNow.Year);
        }

        private static Guid? GetQuestionAnswerId(IQuestionnaire questionnaire, IQuestion question)
        {
            questionnaire.GetQuestionAnswerPairs().TryGetValue(question.Id, out var answer);
            return answer;
        }
    }
}
