using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Repository;
using Hymn_Book.Services;
using Hymn_Book.ViewModels;
using Hymn_Book.Views;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Hymn_Book
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var hymnDb = new HymnDatabase();


            builder.Services.AddSingleton(hymnDb);
            builder.Services.AddScoped<IHymnRepository, HymnRepository>();
            builder.Services.AddScoped<IHymnService, HymnService>();
            builder.Services.AddTransient<HymnListViewModel>();
            builder.Services.AddTransient<HymnListPage>();
            builder.Services.AddTransient<HymnDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var repo = scope.ServiceProvider.GetService<IHymnRepository>();
            if (repo != null)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await HymnSeeder.SeedFromDocxAsync(repo);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Seeder] Failed to seed hymns: {ex.Message}");
                    }
                });
            }

            return app;
        }
    }
}
