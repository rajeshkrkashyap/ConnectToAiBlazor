using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.Views;
using DataModel.Utility;

namespace ConnectToAi.MobileApp.Pages;

public partial class Settings : ContentPage
{
    public Settings(AppSettings appSettings, INavigationService navigationService)
    {
        InitializeComponent();
        stackLayout.Add(new SettingsCV(navigationService));
    }
}