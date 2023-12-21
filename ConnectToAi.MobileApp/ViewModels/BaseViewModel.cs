using CommunityToolkit.Mvvm.ComponentModel;
using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using Gst.Video;
using System;
using System.Collections.Generic;
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

        private HttpClient _httpClient;
        public HttpClient HttpClientProp
        {
            get
            {
                return _httpClient;
            }
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
