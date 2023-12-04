using ConnectToAi.MobileApp.UtilityClasses;
using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp.Views;

public partial class ResponseCV : ContentView
{
    bool _isSpeachOn = true;
    public ResponseCV(string responseText, bool isSpeachOn)
    {
        BindingContext = new ResponseCVViewModel(responseText);
        InitializeComponent();
    }

}