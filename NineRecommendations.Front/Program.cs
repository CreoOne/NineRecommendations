using Microsoft.Extensions.Options;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.External.Options;

namespace NineRecommendations.Front
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<SpotifyOptions>(builder.Configuration.GetSection(nameof(SpotifyOptions)));
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".Que";
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IQuestionnaireRepository, InMemoryQuestionnaireRepository>();
            builder.Services.AddSingleton<IRecommendationRepository, InMemoryRecommendationRepository>();
            builder.Services.AddSingleton<ISpotifyApi>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<SpotifyOptions>>().Value;
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                return new DefaultSpotifyApi(httpClientFactory, options);
            });
            builder.Services.AddSingleton<IRecommendationBuilder>(serviceProvider =>
            {
                var recommendationBuilder = new DefaultRecommendationBuilder();

                var spotifyApi = serviceProvider.GetRequiredService<ISpotifyApi>();
                recommendationBuilder.AddRecommendationBuilder(new Spotify.Recommendations.RecommendationBuilder(spotifyApi));

                return recommendationBuilder;
            });
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Recommendations}/{action=Index}/{id?}");

            app.Run();
        }
    }
}