using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class AudioPlayerViewModel : BaseViewModel
    {
        IAudioPlayer audioPlayer;
        System.Timers.Timer timer;
        public AudioPlayerViewModel(Stream audioStream, INavigationService navigationService):base(navigationService)
        {
            audioPlayer = AudioManager.Current.CreatePlayer(audioStream);
        }

        [ObservableProperty]
        public bool isPlayVisible = true;
        [ObservableProperty]
        public bool isPauseVisible;

        [ObservableProperty]
        public double sliderValue;

        public double currentSliderValue;

        [RelayCommand]
        public void SliderDragged()
        {
            timer.Stop();
            IsPlayVisible = true;
            IsPauseVisible = false;
        }

        [RelayCommand]
        public void Play()
        {
            SliderValue = currentSliderValue;
            IsPlayVisible = false;
            IsPauseVisible = true;
            ProgressBarPosition();
            timer.Start();
            audioPlayer.Play();
        }

        private void ProgressBarPosition()
        {
            timer = new System.Timers.Timer(50);
            timer.Elapsed += ElapsedHandler;
        }

        private void ElapsedHandler(object? sender, ElapsedEventArgs e)
        {
            if (SliderValue < 100)
            {
                SliderValue = (audioPlayer.CurrentPosition * 100) / (audioPlayer.Duration / 1000);
                currentSliderValue = SliderValue;

                if (SliderValue >= 99.9)
                {
                    timer.Stop();
                    SliderValue = 100;
                    IsPlayVisible = true;
                    IsPauseVisible = false;
                    currentSliderValue = 0;
                }
            }
        }

        [RelayCommand]
        public void Pause()
        {
            IsPlayVisible = true;
            IsPauseVisible = false;
            audioPlayer.Pause();
            timer.Stop();
        }
    }
}
