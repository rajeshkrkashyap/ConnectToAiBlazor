using CommunityToolkit.Mvvm.ComponentModel;
using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using Gst.Video;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject, IDisposable
    {
        protected readonly INavigationService NavigationService;
        public BaseViewModel (INavigationService navigationService)
        {
            NavigationService = navigationService;
            PlatformviseDisplay();
        }

        [ObservableProperty]
        public bool _isBusy;
        [ObservableProperty]
        public string _title;

        #region Platform related Display settings
        [ObservableProperty]
        private int sfComboBoxWidthRequest;
        [ObservableProperty]
        private int sfComboBoxHeightRequest;
        [ObservableProperty]
        private Microsoft.Maui.Thickness sfComboBoxMargin;
        private void PlatformviseDisplay()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    GlobalVariables.RuntimePlatform = "Android";
                    SfComboBoxWidthRequest = 70;
                    SfComboBoxHeightRequest = 40;
                    SfComboBoxMargin = new Thickness(0, 35, 0, 0);
                    break;
                case Device.iOS:
                    GlobalVariables.RuntimePlatform = "iOS";
                    break;
                case Device.WinUI:
                    GlobalVariables.RuntimePlatform = "Windows";
                    SfComboBoxWidthRequest = 70;
                    SfComboBoxHeightRequest = 40;
                    SfComboBoxMargin = new Thickness(0, 5, 0, 0);
                    break;
                default:
                    GlobalVariables.RuntimePlatform = "Unknown";
                    break;
            }

        }
        #endregion


        [ObservableProperty]
        private Country selectCountry;
        [ObservableProperty]
        private ObservableCollection<Country> countries;


        private HttpClient _httpClient;
        public HttpClient HttpClientProp
        {
            get
            {
                return _httpClient;
            }
        }
        public void PopulatCountries()
        {
            Countries = new ObservableCollection<Country>
            {
                new Country() { Code = "001", Name = "USA" },
                new Country() { Code = "001", Name = "CAN" },
                new Country() { Code = "044", Name = "UK" },
                new Country() { Code = "061", Name = "AUS" },
                new Country() { Code = "049", Name = "GER" },
                new Country() { Code = "033", Name = "FRA" },
                new Country() { Code = "081", Name = "JPN" },
                new Country() { Code = "055", Name = "BRA" },
                new Country() { Code = "091", Name = "IND" },
                new Country() { Code = "027", Name = "SAF" },
                new Country() { Code = "086", Name = "CHN" },
                new Country() { Code = "007", Name = "RUS" },
                new Country() { Code = "034", Name = "ESP" },
                new Country() { Code = "039", Name = "ITA" },
                new Country() { Code = "052", Name = "MEX" },
                new Country() { Code = "082", Name = "KOR" },
                new Country() { Code = "054", Name = "ARG" },
                new Country() { Code = "234", Name = "NGA" },
                new Country() { Code = "020", Name = "EGY" },
                new Country() { Code = "254", Name = "KEN" },
                new Country() { Code = "966", Name = "SAU" },
                new Country() { Code = "090", Name = "TUR" },
                new Country() { Code = "066", Name = "THA" },
                new Country() { Code = "084", Name = "VNM" },
                new Country() { Code = "062", Name = "IDN" },
                new Country() { Code = "031", Name = "NLD" },
                new Country() { Code = "030", Name = "GRC" },
                new Country() { Code = "046", Name = "SWE" },
                new Country() { Code = "047", Name = "NOR" },
                new Country() { Code = "051", Name = "PER" },
                new Country() { Code = "351", Name = "PRT" },
                new Country() { Code = "048", Name = "POL" },
                new Country() { Code = "353", Name = "IRL" },
                new Country() { Code = "041", Name = "CHE" },
                new Country() { Code = "043", Name = "AUT" },
                new Country() { Code = "032", Name = "BEL" },
                new Country() { Code = "045", Name = "DNK" },
                new Country() { Code = "358", Name = "FIN" },
                new Country() { Code = "064", Name = "NZL" },
                new Country() { Code = "065", Name = "SGP" },
                new Country() { Code = "060", Name = "MYS" },
                new Country() { Code = "063", Name = "PHL" },
                new Country() { Code = "098", Name = "IRN" },
                new Country() { Code = "096", Name = "IRQ" },
                new Country() { Code = "972", Name = "ISR" },
                new Country() { Code = "961", Name = "LBN" },
                new Country() { Code = "211", Name = "SSD" },
                new Country() { Code = "977", Name = "NPL" },
                new Country() { Code = "092", Name = "PAK" },
                new Country() { Code = "880", Name = "BGD" },
                new Country() { Code = "094", Name = "LKA" },
                new Country() { Code = "420", Name = "CZE" },
                new Country() { Code = "036", Name = "HUN" },
                new Country() { Code = "385", Name = "HRV" },
                new Country() { Code = "040", Name = "ROU" },
                new Country() { Code = "359", Name = "BGR" },
                new Country() { Code = "372", Name = "EST" },
                new Country() { Code = "371", Name = "LVA" },
                new Country() { Code = "370", Name = "LTU" },
                new Country() { Code = "381", Name = "SRB" }
            };
            SelectCountry = Countries.First(c => c.Name == "IND");
        }
        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
