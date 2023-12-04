using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;
using Microsoft.Identity.Client;
using System.Security;

namespace ConnectToAi.MobileApp;

public partial class MobileLogin : ContentPage
{
    public MobileLogin(AppSettings appSettings)
    {
        BindingContext = new LoginPageViewModel(appSettings);
        InitializeComponent();
        userName.Text = string.Empty;
    }

    private void BtnClicked_Next(object sender, EventArgs e)
    {
        if (userName.Text.Length > 9)
        {
            UseIdStack.IsVisible = false;
            PasswordStack.IsVisible = true;
            btnNext.Margin = new Thickness(0, 0, 0, 0);
        }else
        {
            lblErrorMessage.IsVisible = true;
            btnNext.Margin = new Thickness(0, -30, 0, 0);
        }
    }

    private void BtnClicked_Back(object sender, EventArgs e)
    {
        UseIdStack.IsVisible = true;
        PasswordStack.IsVisible = false;
    }

    private void userName_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (lblErrorMessage!=null)
        {
            lblErrorMessage.IsVisible = false;
            btnNext.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}