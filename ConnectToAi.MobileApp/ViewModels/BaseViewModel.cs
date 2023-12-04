using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject, IDisposable
    {
        [ObservableProperty]
        public bool _isBusy;
        [ObservableProperty]
        public string _title;

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
