using Syncfusion.Maui.Core.Hosting;
//using CommunityToolkit.Maui;
using ConnectToAi.MobileApp.Pages;
using ConnectToAi.MobileApp.ViewModels;
using ConnectToAi.MobileApp.Views;
using DataModel.Utility;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Services;

namespace ConnectToAi.MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
                //.UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("Font-Awesome- 6-Brands-Regular-400.otf", "FAB");
                    fonts.AddFont("Font-Awesome- 6-Free-Regular-400.otf", "FAR");
                    fonts.AddFont("Font-Awesome- 6-Free-Solid-900.otf", "FAS");

                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AppSettings>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<MobileLogin>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<PromptService>();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<Home>();

            return builder.Build();
        }
    }
}
