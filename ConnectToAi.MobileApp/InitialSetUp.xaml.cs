using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;
using Newtonsoft.Json;

namespace ConnectToAi.MobileApp;

public partial class InitialSetUp : ContentPage
{
    public InitialSetUp(AppSettings appSettings, INavigationService navigationService)
    {
        var mobileNumber = Preferences.Get("MobileNumberKey", "");
        var countryStr = Preferences.Get("SelectCountryKey", "");
        var country = JsonConvert.DeserializeObject<Country>(countryStr);

        BindingContext = new InitialSetUpViewModel(country, Convert.ToInt64(mobileNumber), appSettings, navigationService);
        InitializeComponent();
    }
}