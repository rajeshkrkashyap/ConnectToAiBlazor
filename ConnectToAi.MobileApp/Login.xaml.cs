using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp;

public partial class Login : ContentPage
{
    public Login(AppSettings appSettings)
    {
        BindingContext = new LoginPageViewModel(appSettings);
        InitializeComponent();
    }
}