using Syncfusion.Maui.Core.Hosting;
//using CommunityToolkit.Maui;
using ConnectToAi.MobileApp.Pages;
using ConnectToAi.MobileApp.ViewModels;
using ConnectToAi.MobileApp.Views;
using DataModel.Utility;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Services;
using Microsoft.Maui.Hosting;
using ConnectToAi.MobileApp.Navigation;
using CommunityToolkit.Maui;
using ConnectToAi.MobileApp.UtilityClasses;

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
                .UseMauiCommunityToolkitMediaElement()
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

            //register UI
            //builder.Services.AddSingleton<Login>();
            //builder.Services.AddSingleton<MobileLogin>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddTransient<Home>();
            builder.Services.AddTransient<Settings>();
            builder.Services.AddTransient<InitialSetUp>();
            builder.Services.AddTransient<PaymentPage>();

            //register ViewModel
            builder.Services.AddSingleton<INavigationService, NavigationService>(); 
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<AudioPlayerViewModel>();
            builder.Services.AddTransient<ResponseCVViewModel>();


            // Register other Service
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PromptService>();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<AppSettings>();

            return builder.Build();
        }
    }
}
