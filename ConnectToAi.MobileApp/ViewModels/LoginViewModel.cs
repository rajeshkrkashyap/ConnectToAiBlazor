using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        public LoginPageViewModel(AppSettings appSettings)
        {
            _authService = new AuthService(appSettings);
        }

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string message="";


        [RelayCommand]
        public async Task Login()
        {
            Message = "";
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Email = UserName,
                Password = Password
            };
            var response = await _authService.LoginAsync(loginViewModel);
            if (response != null)
            {
                 LoginOnPostAsync(response);
            }
        }

        private async Task LoginOnPostAsync(HttpResponseMessage response)
        {
            if (response != null && response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null)
                {
                    var mainResponse = JsonConvert.DeserializeObject<MainResponse>(responseBody);

                    if (mainResponse != null && mainResponse.IsSuccess)
                    {
                        if (mainResponse.Content != null)
                        {
                            var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(mainResponse.Content.ToString());
                            if (authenticationResponse != null)
                            {
                                var handler = new JwtSecurityTokenHandler();
                                var jsontoken = handler.ReadToken(authenticationResponse.AccessToken) as JwtSecurityToken;
                                if (!string.IsNullOrWhiteSpace(authenticationResponse.AccessToken))
                                {
                                    string userID = jsontoken.Claims.FirstOrDefault(f => f.Type == JwtRegisteredClaimNames.NameId).Value;
                                    string name = jsontoken.Claims.FirstOrDefault(f => f.Type == JwtRegisteredClaimNames.UniqueName).Value;
                                    string userAvatar = jsontoken.Claims.FirstOrDefault(f => f.Type == "UserAvatar").Value;
                                    string role = jsontoken.Claims.FirstOrDefault(f => f.Type == "role").Value;
                                    string mobileNo = jsontoken.Claims.FirstOrDefault(f => f.Type == "MobileNumber").Value;
                                    string balanceTokens = jsontoken.Claims.FirstOrDefault(f => f.Type == "BalanceTokens").Value;
                                    string subscriptionEndDate = jsontoken.Claims.FirstOrDefault(f => f.Type == "SubscriptionEndDate").Value;
                                    string email = UserName;

                                    var userDetail = new UserDetail
                                    {
                                        Email = email,
                                        Name = name,
                                        Role = role,
                                        AccessToken = authenticationResponse.AccessToken,
                                        RefreshToken = authenticationResponse.RefreshToken,
                                        UserAvatar = !string.IsNullOrWhiteSpace(userAvatar) ? $"{ApiUrl.ApiBaseURL}/{userAvatar}" : "",
                                        UserID = userID,
                                        Tokens = Convert.ToDecimal(balanceTokens),
                                        //AppSettingCookie = appSettingCookie
                                    };

                                    string userDetailInfoStr = JsonConvert.SerializeObject(userDetail);
                                    
                                    Preferences.Set("UserLoggedInKey", userDetailInfoStr);

                                 //   await UserCookiesManagement(null, subscriptionEndDate, userDetail);
                                }
                                else
                                {
                                    //ModelState.AddModelError(string.Empty, "Invalid Token created");
                                    Message = "Invalid Token created";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mainResponse != null && mainResponse.ErrorMessage != null)
                        {
                            //ModelState.AddModelError(string.Empty, mainResponse.ErrorMessage);
                            Message = mainResponse.ErrorMessage;
                        }
                    }
                }
            }
            else
            {
                if (response != null && response.ReasonPhrase != null)
                {
                    //ModelState.AddModelError(string.Empty, response.ReasonPhrase);
                    Message = response.ReasonPhrase;
                }
            }
        }
    }
}
