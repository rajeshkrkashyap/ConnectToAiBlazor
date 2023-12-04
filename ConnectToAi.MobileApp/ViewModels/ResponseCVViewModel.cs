using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class ResponseCVViewModel : BaseViewModel
    {
        public ResponseCVViewModel(string responseText)
        {
            ResponseText = responseText;
            VolumeOn();
        }

        [ObservableProperty]
        public bool isHighVolumeVisible = true;
        [ObservableProperty]
        public bool isMuteVolumeVisible;
        [ObservableProperty]
        public string responseText;

        [RelayCommand]
        public async Task VolumeOn()
        {
            IsHighVolumeVisible = true;
            IsMuteVolumeVisible = false;
            if (ResponseText != null)
            {
                await TextToSpeachServices.TextToSpeachSynthesizer(ResponseText, false);
                IsHighVolumeVisible = false;
                IsMuteVolumeVisible = true;
            }
        }

        [RelayCommand]
        public async Task VolumeOff()
        {
            IsHighVolumeVisible = true;
            IsMuteVolumeVisible = false;
            await TextToSpeachServices.TextToSpeachSynthesizer(ResponseText, true);
            IsHighVolumeVisible = false;
            IsMuteVolumeVisible = true;
        }
    }
}
