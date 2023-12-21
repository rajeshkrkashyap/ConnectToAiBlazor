using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.ViewModels;
using Newtonsoft.Json;

namespace ConnectToAi.MobileApp.Views;

public partial class SettingsCV : ContentView
{
	public SettingsCV(INavigationService navigationService)
    {
        BindingContext = new SettingsViewModel(navigationService);
        InitializeComponent();
    }
   

    private void ComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        string aiAvatarStr = JsonConvert.SerializeObject((Avatar)e.CurrentSelection[0]);
        Preferences.Set("AiAvatarName", aiAvatarStr);
    }
}