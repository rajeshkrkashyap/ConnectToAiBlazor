using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using ConnectToAi.MobileApp.Views;
using DataModel.Utility;
using Plugin.Maui.Audio;


namespace ConnectToAi.MobileApp.Pages;

public partial class Home : ContentPage
{
    public Home(IAudioManager audioManager, AppSettings appSettings, INavigationService navigationService)
    {
        InitializeComponent();
        stackLayout.Add(new MicCV(audioManager, appSettings, navigationService));
    }
}