using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.Questionnaries;

namespace NineRecommendations.Front.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntryQuestion(this IServiceCollection services)
        {
            services.AddSingleton<IFinder, DefaultFinder>(serviceProvider => new DefaultFinder(new EntryQuestion(Answers.Spotify)));
            return services;
        }
        public static IServiceCollection AddRecommendationBuilder(this IServiceCollection services)
        {
            services.AddSingleton<IRecommendationBuilder>(serviceProvider =>
            {
                var recommendationBuilder = new DefaultRecommendationBuilder();

                var spotifyApi = serviceProvider.GetRequiredService<ISpotifyApi>();
                recommendationBuilder.AddRecommendationBuilder(new Spotify.Recommendations.RecommendationBuilder(spotifyApi));

                return recommendationBuilder;
            });

            return services;
        }
    }
}
