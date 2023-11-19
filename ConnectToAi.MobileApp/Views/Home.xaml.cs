using Azure.Core;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Layouts;
using Plugin.Maui.Audio;
using System.Reflection.Metadata.Ecma335;


namespace ConnectToAi.MobileApp.Views;

public partial class Home : ContentPage
{
    private int totalTime = 100; // Set the total time of your audio in seconds
    readonly IAudioManager _audioManager;
    readonly IAudioRecorder _audioRecorder;

    private List<AudioPayer> contentList;
    public Home(IAudioManager audioManager)
    {
        InitializeComponent();
        _audioManager = audioManager;
        _audioRecorder = audioManager.CreateRecorder();
        contentList = new List<AudioPayer>();
    }

    private void DynamicButton_Clicked(object sender, EventArgs e)
    {
        // Handle button click event
        // You can add more controls or perform other actions here
    }
    private void ImageOffButton_Clicked(object sender, EventArgs e)
    {
        imgOffMicButton.IsVisible = false;
        imgOnMicButton.IsVisible = true;

        this.listViewItem.Children.Add(new AudioPayer());
    }
    private void ImageOnButton_Clicked(object sender, EventArgs e)
    {
        imgOffMicButton.IsVisible = true;
        imgOnMicButton.IsVisible = false;
        //PayPauseSound(false);
    }

    private async Task<bool> GetPermission()
    {
        var status = PermissionStatus.Unknown;
        status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
        if (status == PermissionStatus.Granted)
        {
            return true;

        }
        status = await Permissions.RequestAsync<Permissions.Microphone>();

        if (status != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Permission required",
                    "Microphone permission is required for recording",
                    "We donot use your voice other than generating response of you question", "OK");
            return false;
        }

        return false;
    }
    private async void PayPauseSound(bool isPlay)
    {
        if (!await GetPermission())
        {
            return;
        }

        if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
        {
            return;
        }

        if (!_audioRecorder.IsRecording)
        {
            await _audioRecorder.StartAsync();
        }
        else
        {
            var recordredAudio = await _audioRecorder.StopAsync();
            var player = AudioManager.Current.CreatePlayer(recordredAudio.GetAudioStream());
            player.Play();
        }
    }
}