 
//using Android.Provider;
//using Android.SE.Omapi;
using ConnecttoAi.Data;
using DataModel.Models;
using DataModel.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ConnecttoAi
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
                });
#if DEBUG
            builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#endif
            var configuration = builder.Configuration;
            var appSettingValues = new AppSettings();
            configuration.GetSection("Settings").Bind(appSettingValues);
            builder.Services.AddSingleton(appSettingValues);

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddMudServices();

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<DataService>();



#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}