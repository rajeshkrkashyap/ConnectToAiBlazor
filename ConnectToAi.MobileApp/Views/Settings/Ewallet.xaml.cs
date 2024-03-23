using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.ViewModels;

namespace ConnectToAi.MobileApp.Views.Settings;

public partial class Ewallet : ContentView
{
    public Ewallet(INavigationService navigationService)
    {
        BindingContext = new EwalletViewModel(navigationService);
        InitializeComponent();
        ((Entry)(txtAddMoney.Content)).Text = string.Empty;
    }

    private void txtAddMoney_Focused(object sender, FocusEventArgs e)
    {

    }
}