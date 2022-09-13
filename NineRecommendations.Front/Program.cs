using NineRecommendations.Core.Extensions;
using NineRecommendations.Spotify.Extensions;
using NineRecommendations.Spotify.External;
using NineRecommendations.Spotify.Questionnaries;
using NineRecommendations.Spotify.Recommendations;

namespace NineRecommendations.Front
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".Que";
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpClient();

            builder.Services.AddSpotifyProvider(builder.Configuration);
            builder.Services.AddEntryQuestion(Answers.Spotify);
            builder.Services.AddQuestionnairesAndRecommendationsPersistence();
            builder.Services.AddRecommendationBuilder((serviceProvider, recommendationBuilder) =>
            {
                var spotifyApi = serviceProvider.GetRequiredService<ISpotifyApi>();
                recommendationBuilder.AddRecommendationBuilder(new RecommendationBuilder(spotifyApi));
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