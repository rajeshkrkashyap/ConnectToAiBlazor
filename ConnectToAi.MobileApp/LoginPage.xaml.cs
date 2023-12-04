using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp;

public partial class LoginPage : ContentPage
{
    public LoginPage(AppSettings appSettings)
    {
        BindingContext = new LoginPageViewModel(appSettings);
        InitializeComponent();
        ((Entry)(txtMobileNumber.Content)).Text = string.Empty;
        ((Entry)(txtOTP.Content)).Text = string.Empty;
    }
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= 6)
        {
            loginButton.IsEnabled = true;
            loginButton.Background = Colors.Green;
        }
    }
}