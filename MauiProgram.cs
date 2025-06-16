using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Services;
using Hymn_Book.ViewModels;
using Hymn_Book.Views;
using Microsoft.Extensions.Logging;

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
            builder.Services.AddScoped<IHymnRepository, IHymnRepository>();
            builder.Services.AddScoped<IHymnService, IHymnService>();
            builder.Services.AddTransient<HymnListViewModel>();
            builder.Services.AddTransient<HymnListPage>();
            builder.Services.AddTransient<HymnDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
