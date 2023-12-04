using ConnectToAi.MobileApp.ViewModels;
using DataModel.Utility;

namespace ConnectToAi.MobileApp.Views;

public partial class AudioPayerAndriod : ContentView
{
	public AudioPayerAndriod(AudioPlayerViewModel audioPlayerViewModel)
	{
        BindingContext = audioPlayerViewModel;
        InitializeComponent();
	}   
}