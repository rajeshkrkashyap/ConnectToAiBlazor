using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using ConnectToAi.MobileApp.ViewModels;
using DataModel.Models;
using DataModel.Utility;
using Plugin.Maui.Audio;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace ConnectToAi.MobileApp.Views;

public partial class MicCV : ContentView
{
    //private int totalTime = 100; // Set the total time of your audio in seconds
    readonly IAudioRecorder _audioRecorder;

    //const string subscription = "13296b15a6a447ef8d1d904138a560d5";
    //const string region = "centralus";
    private INavigationService navigationService;
    public MicCV(IAudioManager audioManager, AppSettings appSettings, INavigationService _navigationService)
    {
        navigationService = _navigationService;
        BindingContext = new HomeViewModel(appSettings, navigationService);

        InitializeComponent();
        _audioRecorder = audioManager.CreateRecorder();

        if (Device.RuntimePlatform == Device.WinUI)
        {
            imgOffMicButton.HeightRequest = 120;
            imgOnMicButton.HeightRequest = 120;
        }
        else if (Device.RuntimePlatform == Device.Android)
        {
            imgOffMicButton.HeightRequest = 80;
            imgOnMicButton.HeightRequest = 80;
        }

    }
    private ScrollView MyScrollView => (ScrollView)FindByName("myScrollView");

    private async void ImageOffButton_Clicked(object sender, EventArgs e)
    {
        imgOffMicButton.IsVisible = false;
        imgOnMicButton.IsVisible = true;

        if (!await GetMicPermission())
        {
            imgOffMicButton.IsVisible = true;
            imgOnMicButton.IsVisible = false;
            return;
        }

        if (!_audioRecorder.IsRecording)
        {
            await _audioRecorder.StartAsync();


            string speachText = await SpeachRecognition.RecognitionWithMicrophoneAsync();

            ////await SynthesisToSpeakerAsync(speachText);
            string responseText = "";
           
            if (speachText.Length > 0)
            {
                var context = (HomeViewModel)BindingContext;
                responseText = await UpdateUiWithOutStream(await context.SendPromtCommand(speachText));
            }
            else
            {
                responseText = "Sorry no response!";
            }

            this.listViewItem.Children.Add(new ResponseCV(responseText, navigationService));

            await MyScrollView.ScrollToAsync(-20, MyScrollView.ContentSize.Height, true);
        }
    }
  
    private async void ImageOnButton_Clicked(object sender, EventArgs e)
    {
        imgOffMicButton.IsVisible = true;
        imgOnMicButton.IsVisible = false;
        var recordredAudio = await _audioRecorder.StopAsync();
        var STREAM = recordredAudio.GetAudioStream();
        if (Device.RuntimePlatform == Device.WinUI)
        {
            this.listViewItem.Children.Add(new AudioPayerWindows(new AudioPlayerViewModel(STREAM, navigationService)));
        }
        else
        {
            this.listViewItem.Children.Add(new AudioPayerAndriod(new AudioPlayerViewModel(STREAM, navigationService)));
        }

        await MyScrollView.ScrollToAsync(-20, MyScrollView.ContentSize.Height, true);
        //var text = await SpeachRecognition.RecognitionWithMicrophoneAsync();
        //var context = (HomeViewModel)BindingContext;
        //speachText = await SpeachRecognition.SpeechRecognitionWithCompressedInputPullStreamAudio(STREAM);
        //await context.SendPromtCommand(speachText);
        //speachText = await SpeachRecognition.RecognitionWithMicrophoneAsync();
    }
    protected async Task<string> UpdateUiWithOutStream(HttpResponseMessage? response)
    {
        try
        {
            if (response != null && response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        var mainResponse = await reader.ReadToEndAsync();
                        var chatCompletion = System.Text.Json.JsonSerializer.Deserialize<ChatCompletion>(mainResponse);
                        if (chatCompletion != null && chatCompletion.choices.Count() > 0)
                        {
                            return chatCompletion.choices[0].message.content;
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {

        }
        return "failed";
    }
    //public static async Task SynthesisToSpeakerAsync(string speachText)
    //{
    //    // To support Chinese Characters on Windows platform
    //    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    //    {
    //        Console.InputEncoding = System.Text.Encoding.Unicode;
    //        Console.OutputEncoding = System.Text.Encoding.Unicode;
    //    }

    //    // Creates an instance of a speech config with specified subscription key and service region.
    //    // Replace with your own subscription key and service region (e.g., "westus").
    //    // The default language is "en-us".
    //    var config = SpeechConfig.FromSubscription(subscription, region);

    //    // Set the voice name, refer to https://aka.ms/speech/voices/neural for full list.
    //    config.SpeechSynthesisVoiceName = "en-US-AriaNeural";

    //    // Creates a speech synthesizer using the default speaker as audio output.
    //    using (var synthesizer = new SpeechSynthesizer(config))
    //    {
    //        // Receive a text from console input and synthesize it to speaker.
    //        Console.WriteLine("Type some text that you want to speak...");
    //        Console.Write("> ");
    //        string text = speachText; //Console.ReadLine();

    //        using (var result = await synthesizer.SpeakTextAsync(text))
    //        {
    //            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
    //            {
    //                Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
    //            }
    //            else if (result.Reason == ResultReason.Canceled)
    //            {
    //                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
    //                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

    //                if (cancellation.Reason == CancellationReason.Error)
    //                {
    //                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
    //                    Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
    //                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
    //                }
    //            }
    //        }

    //        // This is to give some time for the speaker to finish playing back the audio
    //        Console.WriteLine("Press any key to exit...");
    //        // Console.ReadKey();
    //    }
    //}
    private static async Task<bool> GetMicPermission()
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
    //private async Task<Stream> StartRecording()
    //{
    //    if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
    //    {
    //        return null;
    //    }


    //    return null;
    //}
}