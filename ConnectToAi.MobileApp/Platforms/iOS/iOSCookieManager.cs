using ConnectToAi.MobileApp.Platforms.iOS;
using ConnectToAi.MobileApp.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using WebKit;

[assembly: Dependency(typeof(IOSCookieManager))]
namespace ConnectToAi.MobileApp.Platforms.iOS
{
    public class IOSCookieManager : ICookieManager
    {
        public void SetCookie(string domain, string cookieName, string cookieValue)
        {
            var cookieProperties = new NSMutableDictionary();
            cookieProperties.Add(NSHttpCookie.KeyDomain, new NSString(domain));
            cookieProperties.Add(NSHttpCookie.KeyPath, new NSString("/"));
            cookieProperties.Add(NSHttpCookie.KeyName, new NSString(cookieName));
            cookieProperties.Add(NSHttpCookie.KeyValue, new NSString(cookieValue));

            var cookie = new NSHttpCookie(cookieProperties);
            var cookieStorage = NSHttpCookieStorage.SharedStorage;
            cookieStorage.SetCookie(cookie);
        }
        public string GetCookie(string domain, string cookieName)
        {
            var cookieStorage = NSHttpCookieStorage.SharedStorage;
            var cookies = cookieStorage.CookiesForUrl(new NSUrl(domain));

            foreach (var cookie in cookies)
            {
                if (cookie.Name == cookieName)
                {
                    return cookie.Value;
                }
            }

            return null;
        }
    }
}
