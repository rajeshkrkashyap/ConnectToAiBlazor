using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.UtilityClasses
{
    public interface ICookieManager
    {
        void SetCookie(string domain, string cookieName, string cookieValue);
        // Other cookie-related methods can be added here
        string GetCookie(string domain, string cookieName);
    }


}
