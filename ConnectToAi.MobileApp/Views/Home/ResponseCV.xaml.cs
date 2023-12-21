using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp.Views;

public partial class ResponseCV : ContentView
{
    bool _isSpeachOn = true;
    public ResponseCV(string responseText, INavigationService navigationService)
    {
        BindingContext = new ResponseCVViewModel(responseText, navigationService);
        InitializeComponent();
    }
}