using App.Service;
using App.Service.Interfaces;
using Microsoft.Extensions.Logging;
using App.Views;
using App.ViewModels;

namespace App
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
            //Conectivy
            builder.Services.AddSingleton(Connectivity.Current);
            //Services 
            builder.Services.AddHttpClient<IHomeService, HomeService>().SetHandlerLifetime(TimeSpan.FromMinutes(5));
            //Views
            builder.Services.AddSingleton<Home>();
            //ViewModels
            builder.Services.AddSingleton<HomeViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}