using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp.Views;

public partial class AudioPayerWindows : ContentView
{
	public AudioPayerWindows(AudioPlayerViewModel audioPlayerViewModel)
	{
        BindingContext = audioPlayerViewModel;
        InitializeComponent();
	}   
}