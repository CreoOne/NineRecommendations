using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.External.Options;

namespace NineRecommendations.Spotify.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Spotify Api related dependencies
        /// </summary>
        public static IServiceCollection AddSpotifyProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SpotifyOptions>(configuration.GetSection(nameof(SpotifyOptions)));
            services.AddSingleton<ISpotifyApi>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<SpotifyOptions>>().Value;
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                return new DefaultSpotifyApi(httpClientFactory, options);
            });

            return services;
        }
    }
}
