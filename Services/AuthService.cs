using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Services
{
    public class AuthService:BaseService
    {
        public AuthService(AppSettings appSettings) :base(appSettings)
        {

        }
        public async Task<HttpResponseMessage> LoginAsync(LoginViewModel loginViewModel)
        {
            if (loginViewModel != null)
            {
                var jsonData = JsonConvert.SerializeObject(loginViewModel);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.Login}";
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        return await httpClient.PostAsync(url, content);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            // If we got this far, something failed, redisplay form
            return null;
        }
    }
}