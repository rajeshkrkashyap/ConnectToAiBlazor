using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;
using Microsoft.Identity.Client;
using System.Security;

namespace ConnectToAi.MobileApp;

public partial class Login : ContentPage
{
    public Login(AppSettings appSettings, INavigationService navigationService)
    {
        BindingContext = new LoginPageViewModel(appSettings, navigationService);
        InitializeComponent();
    }
}