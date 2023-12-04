using ConnectToAi.MobileApp.UtilityClasses;

namespace ConnectToAi.MobileApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {// Call the method to detect and display the platform
            DetectAndDisplayPlatform();
            InitializeComponent();

        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private void DetectAndDisplayPlatform()
        {
            // Check the runtime platform
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    GlobalVariables.RuntimePlatform = "Android";
                    break;
                case Device.iOS:
                    GlobalVariables.RuntimePlatform = "iOS";
                    break;
                case Device.WinUI:
                    GlobalVariables.RuntimePlatform = "Windows";
                    break;
                default:
                    GlobalVariables.RuntimePlatform = "Unknown";
                    break;
            }

        }
    }
}
