using NineRecommendations.Core.Persistence;

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
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.IsEssential = false;
            });

            builder.Services.AddSingleton<IQuestionnaireRepository, InMemoryQuestionnaireRepository>();
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}