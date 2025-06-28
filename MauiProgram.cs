using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;
using Hymn_Book.Repository;
using Hymn_Book.Services;
using Hymn_Book.ViewModels;
using Hymn_Book.Views;
using Hymn_Book.Views.Page;
using Microsoft.Extensions.Logging;
using SQLite;
using System.Diagnostics;

namespace Hymn_Book;

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

        // Register SQLite connection
        builder.Services.AddSingleton(provider =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db");
            var conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Hymn>().Wait();
            return conn;
        });

        // Register app services
        builder.Services.AddSingleton<HymnDatabase>();
        builder.Services.AddScoped<IHymnRepository, HymnRepository>();
        builder.Services.AddScoped<IHymnService, HymnService>();

        // Register view models
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<HymnListViewModel>();
        builder.Services.AddTransient<HymnSearchViewModel>();

        // Register views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<HymnDetailPage>();
        builder.Services.AddTransient<HymnListPage>();
        builder.Services.AddTransient<HymnSearchPage>();
        builder.Services.AddSingleton<AppShell>();

        // Register App
        builder.Services.AddSingleton<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();

        // Seed hymns
        Task.Run(async () =>
        {
            using var scope = app.Services.CreateScope();
            var repo = scope.ServiceProvider.GetService<IHymnRepository>();

            if (repo != null)
            {
                try
                {
                    await repo.ResetDatabase();
                    await repo.CreateDatabase();
                    await HymnSeeder.SeedFromJsonAsync(repo);
                    Debug.WriteLine("[Seeder] Hymn seeding completed.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Seeder] Failed to seed hymns: {ex.Message}");
                }
            }
        });

        return app;
    }
}