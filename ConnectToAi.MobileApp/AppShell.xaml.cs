using ConnectToAi.MobileApp.Pages;
using ConnectToAi.MobileApp.UtilityClasses;
using Microsoft.Maui.Controls.Internals;

namespace ConnectToAi.MobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(InitialSetUp), typeof(InitialSetUp));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
            var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");

            if (userDetailInfoStr.Length > 0)
            {
                var page = Preferences.Get("LastPageKey", "");
                switch (page)
                {
                    case "Home":
                        mainShell.CurrentItem = homePage;
                        break;
                    case "Settings":
                        mainShell.CurrentItem = settingsPage;
                        break;
                    case "InitialSetUp":
                        mainShell.CurrentItem = initialSetUp;
                        initialSetUp.IsVisible = true;
                        break;
                    //case "Payment":
                    //    mainShell.CurrentItem = paymentPage;
                    //    break;
                    default:
                        mainShell.CurrentItem = settingsPage;
                        break;
                }

                mobileLoginPage.IsVisible = false;
            }
            else
            {
                mobileLoginPage.IsVisible = true;
                mainShell.CurrentItem = mobileLoginPage;
            }
        }     
    }
}
