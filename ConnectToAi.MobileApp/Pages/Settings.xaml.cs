using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.Views;
using ConnectToAi.MobileApp.Views.Settings;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;

namespace ConnectToAi.MobileApp.Pages;

public partial class Settings : ContentPage
{
    public Settings(AppSettings appSettings, INavigationService navigationService)
    {
        InitializeComponent();
        var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
        if (!string.IsNullOrEmpty(userDetailInfoStr))
        {
            var userDetail = JsonConvert.DeserializeObject<UserDetail?>(userDetailInfoStr);
            if (userDetail != null)
            {
                MyWebView.Source = new UrlWebViewSource { Url = "https://connectto.ai/home/AutoRedirect/?accessToken=" + userDetail.AccessToken };
            }
        }
        //StackLayoutProp = new StackLayout();
        //// You can handle the Navigated event to set cookies after the WebView has navigated to a page
        //MyWebView.Navigated += WebView_Navigated;

        //StackLayoutProp.Children.Add(MyWebView);
        ////Content = new StackLayout
        ////{
        ////    Children = { MyWebView }
        ////};
    }

    //private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
    //{
    //    // Access the CookieManager from the WebView's platform-specific implementation
    //    //ICookieManager cookieManager = new ConnectToAi.MobileApp.AndroidCookieManager(); // Choose the appropriate platform implementation

    //    // Check if the CookieManager is available
    //    //if (cookieManager != null)
    //    //{
    //    // Set a cookie
    //    //cookieManager.SetCookie("https://connectto.ai/", "UserLoggedInKey", userDetailInfoStr);
    //    //}

    //    string javascriptCode = "document.cookie";
    //    string cookies = await MyWebView.EvaluateJavaScriptAsync(javascriptCode);
    //}

    public StackLayout StackLayoutProp { get; set; }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GetCookiesFromWebView();
    }
    private async void GetCookiesFromWebView()
    {
        try
        {
            //JavaScript code to get cookies
            string jsCode = "document.cookie";
            //Evaluate the JavaScript code in the WebView
            App.CookieStr = await MyWebView.EvaluateJavaScriptAsync(jsCode);

            //Add to Collection
            if (!string.IsNullOrEmpty(App.CookieStr))
            {
                var strCookies = App.CookieStr;
                App.CookieContainer = strCookies.Split(";");
            }

            if (App.CookieContainer != null)
            {
                foreach (var item in App.CookieContainer)
                {
                    if (item.Contains("AiAvatarName"))
                    {
                        var aiAvatarName = item.Split("=")[1];
                        Preferences.Set("AiAvatarName", aiAvatarName);
                        break;
                    }
                }
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting cookies: {ex.Message}");
        }
    }

}