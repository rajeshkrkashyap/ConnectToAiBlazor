//using Org.BouncyCastle.Asn1.X509;
using System.Resources;

namespace ConnecttoAi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            // Check the runtime platform
            string platform = Device.RuntimePlatform;

            // Load platform-specific CSS
            if (platform == Device.iOS)
            {
                // Load iOS-specific CSS
                LoadCSS("ios.css");
            }
            else if (platform == Device.Android)
            {
                // Load Android-specific CSS
                LoadCSS("android.css");
            }
            else if (platform == Device.UWP)
            {
                // Load UWP-specific CSS
                LoadCSS("dark.css");
            }else
            {
                LoadCSS("dark.css");
            }
        }
        private void LoadCSS(string fileName)
        {
            // Load and apply the CSS file
            var css = new Style(typeof(VisualElement));
            css.ApplyToDerivedTypes = true;
            css.Setters.Add(new Setter { Property = VisualElement.StyleProperty, Value = fileName });
            Resources.Add(css);
        }
    }
}