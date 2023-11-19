namespace ConnectToAi.MobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");

            if (userDetailInfoStr.Length > 0)
            {
                mainShell.CurrentItem = homePage;
                loginPage.IsVisible = false;
            }
            else
            {
                loginPage.IsVisible = true;
                mainShell.CurrentItem = loginPage;
            }
        }
    }
}
