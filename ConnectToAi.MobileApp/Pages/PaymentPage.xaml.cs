using ConnectToAi.MobileApp.UtilityClasses;
using DataModel.Models;
using Newtonsoft.Json;

namespace ConnectToAi.MobileApp.Pages;

public partial class PaymentPage : ContentPage
{
    public PaymentPage()
    {
        InitializeComponent();
        var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
        if (userDetailInfoStr != null)
        {
            var userDetail = JsonConvert.DeserializeObject<UserDetail?>(userDetailInfoStr);
            MyWebView.Source = new UrlWebViewSource { Url = "https://connectto.ai/avatar/settings/Index/?accessToken=" + userDetail.AccessToken };
        }

        // You can handle the Navigated event to set cookies after the WebView has navigated to a page
        MyWebView.Navigated += WebView_Navigated;

        Content = new StackLayout
        {
            Children = { MyWebView }
        };
    }

    private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        //Access the CookieManager from the WebView's platform-specific implementation
        //ICookieManager cookieManager = new ConnectToAi.MobileApp.AndroidCookieManager(); // Choose the appropriate platform implementation

        //Check if the CookieManager is available
        //if (cookieManager != null)
        //{
        // Set a cookie
        // cookieManager.SetCookie("https://connectto.ai/", "UserLoggedInKey", userDetailInfoStr);
        //}

        string javascriptCode = "document.cookie";
        string cookies = await MyWebView.EvaluateJavaScriptAsync(javascriptCode);
    }

   
}
