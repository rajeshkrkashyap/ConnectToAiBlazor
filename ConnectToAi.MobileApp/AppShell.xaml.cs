namespace ConnectToAi.MobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
            Preferences.Remove("UserLoggedInKey");
            Preferences.Clear();

            if (userDetailInfoStr.Length > 0)
            {
                mainShell.CurrentItem = homePage;
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
