using Android.Webkit;
using ConnectToAi.MobileApp;
using ConnectToAi.MobileApp.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Dependency(typeof(AndroidCookieManager))]
namespace ConnectToAi.MobileApp
{
    public class AndroidCookieManager : ICookieManager
    {
        public void SetCookie(string domain, string cookieName, string cookieValue)
        {
            CookieManager.Instance.SetCookie(domain, $"{cookieName}={cookieValue}");
        }
        public string GetCookie(string domain, string cookieName)
        {
              string cookies = CookieManager.Instance.GetCookie(domain);
            string[] cookieArray = cookies?.Split(';');

            if (cookieArray != null)
            {
                foreach (var cookie in cookieArray)
                {
                    if (cookie.Trim().StartsWith($"{cookieName}="))
                    {
                        return cookie.Trim().Substring($"{cookieName}=".Length);
                    }
                }
            }

            return null;
        }
    }
}
