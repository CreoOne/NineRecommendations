using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Core.Recommendations.Primitives;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.External.Models;
using NineRecommendations.Spotify.External.Options;
using NineRecommendations.Spotify.Questionnaries;
using NineRecommendations.Spotify.Recommendations;
using NSubstitute;

namespace NineRecommendations.Spotify.UnitTests.Recommendations
{
    public class RecommendationTests
    {
        [Fact]
        public void RecommendationIsGeneratedCorrectlyFromSpotifyResponse()
        {
            // arrange

            var questionnaire = new DefaultQuestionnaire(Guid.NewGuid());
            questionnaire.AddAnswer(Questions.Activity.Id, Answers.Explore.Id);
            questionnaire.AddAnswer(Questions.Time.Id, Answers.Timeless.Id);
            var spotifyApi = Substitute.For<ISpotifyApi>();
            spotifyApi
                .CallSearchAsync(Arg.Any<SearchOptions>())
                .Returns(Task.FromResult((RootObject?)new RootObject { Tracks = new Tracks { Items = new[] { new Item { Name = "Example Track" } } } }));
            var recommendation = new Recommendation(Guid.NewGuid(), "Lorem Ipsum", questionnaire, spotifyApi);

            var recommendationTaskWaiting = CreateProcessingWaitTask(recommendation);
            var timeoutTask = CreateTimeoutTask();
            Task.WaitAny(recommendationTaskWaiting, timeoutTask);

            // act

            var actual = recommendation.Recommendations;

            // assert

            Assert.Equal("Example Track", actual.First().Name);
        }

        [Fact]
        public void RecommendationContainsErrorStatusWhenExeptionIsThrown()
        {
            // arrange

            var questionnaire = new DefaultQuestionnaire(Guid.NewGuid());
            questionnaire.AddAnswer(Questions.Activity.Id, Answers.Explore.Id);
            questionnaire.AddAnswer(Questions.Time.Id, Answers.Timeless.Id);
            var spotifyApi = Substitute.For<ISpotifyApi>();
            spotifyApi
                .CallSearchAsync(Arg.Any<SearchOptions>())
                .Returns<Task<RootObject?>>(_ => { throw new Exception(); });
            var recommendation = new Recommendation(Guid.NewGuid(), "Lorem Ipsum", questionnaire, spotifyApi);

            var recommendationTaskWaiting = CreateProcessingWaitTask(recommendation);
            var timeoutTask = CreateTimeoutTask();
            Task.WaitAny(recommendationTaskWaiting, timeoutTask);

            // act

            var actual = recommendation.Status;

            // assert

            Assert.Equal(RecommendationStatus.Error, actual);
        }

        [Fact]
        public void RecommendationContainsEmptyStatusWhenExternalSearchResultIsEmpty()
        {
            // arrange

            var questionnaire = new DefaultQuestionnaire(Guid.NewGuid());
            questionnaire.AddAnswer(Questions.Activity.Id, Answers.Explore.Id);
            questionnaire.AddAnswer(Questions.Time.Id, Answers.Timeless.Id);
            var spotifyApi = Substitute.For<ISpotifyApi>();
            spotifyApi
                .CallSearchAsync(Arg.Any<SearchOptions>())
                .Returns(Task.FromResult((RootObject?)new RootObject()));
            var recommendation = new Recommendation(Guid.NewGuid(), "Lorem Ipsum", questionnaire, spotifyApi);

            var recommendationTaskWaiting = CreateProcessingWaitTask(recommendation);
            var timeoutTask = CreateTimeoutTask();
            Task.WaitAny(recommendationTaskWaiting, timeoutTask);

            // act

            var actual = recommendation.Status;

            // assert

            Assert.Equal(RecommendationStatus.Empty, actual);
        }

        private static Task CreateTimeoutTask() => Task.Delay(TimeSpan.FromSeconds(10));
        private static Task CreateProcessingWaitTask(IRecommendation recommendation) => Task.Factory.StartNew(() =>
        {
            while (recommendation.Status == RecommendationStatus.Processing)
            {
                Task.Delay(TimeSpan.FromMilliseconds(100)).Wait();
            }
        });
    }
}
