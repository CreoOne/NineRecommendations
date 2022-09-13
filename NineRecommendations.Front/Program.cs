using NineRecommendations.Core.Extensions;
using NineRecommendations.Front.Extensions;
using NineRecommendations.Spotify.Extensions;

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
            builder.Services.AddEntryQuestion();
            builder.Services.AddQuestionnairesAndRecommendationsPersistence();
            builder.Services.AddRecommendationBuilder();

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