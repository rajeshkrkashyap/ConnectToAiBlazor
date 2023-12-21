using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace ConnectToAi.MobileApp.Navigation
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            Preferences.Set("LastPageKey", route);
            if (routeParameters != null)
            {
                return Shell.Current.GoToAsync(route, routeParameters);
            }
            else
            {
                return Shell.Current.GoToAsync(route);
            }
        }
    }
}
