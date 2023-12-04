using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Services
{
    public class AuthService : BaseService
    {
        public AuthService(AppSettings appSettings) : base(appSettings)
        {

        }

        public async Task<HttpResponseMessage> MobileLoginAsync(LoginRegisterMobileViewModel loginViewModel)
        {
            if (loginViewModel != null)
            {
                var jsonData = JsonConvert.SerializeObject(loginViewModel);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.MobileLogin}";
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
        public async Task<HttpResponseMessage> RegisterAsync(RegisterViewModel signUpViewModel)
        {
            if (signUpViewModel != null)
            {
                signUpViewModel.EmailConfirmUrl = $"{AppSettings.ApiBaseUrl}/confirmemail";

                var jsonData = JsonConvert.SerializeObject(signUpViewModel);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.Register}";
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

        public async Task<HttpResponseMessage> ConfirmEmail(Uri uri)
        {
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("userid", out var userId);
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var token);

            var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ConfirmEmail}";
            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.GetAsync(url + "/?userId=" + userId + "&token=" + token);
            }
        }

    }
}