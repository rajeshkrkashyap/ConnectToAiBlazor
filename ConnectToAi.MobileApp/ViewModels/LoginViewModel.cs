using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.UtilityClasses;
using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.Pages;


namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly SMSService _sMSService;
        public LoginPageViewModel(AppSettings appSettings, INavigationService navigationService) : base(navigationService)
        {
            _authService = new AuthService(appSettings);
            _sMSService = new SMSService(appSettings);
            PopulatCountries();
        }

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string displayMessage = "Enter your mobile number to proceed.";

        #region Form Fields
        [ObservableProperty]
        private Int64 mobileNumber;
        [ObservableProperty]
        private int oTP;

        #endregion

        #region Page Properties
        [ObservableProperty]
        private bool isNext = true;
        [ObservableProperty]
        private bool isOTP;
        [ObservableProperty]
        private Color textDsplayColor;
        [ObservableProperty]
        private string message = "";
        [ObservableProperty]
        private Timer displayTimer;
        [ObservableProperty]
        private int seconds = 20;
        [ObservableProperty]
        private bool isEnabled = false;
        [ObservableProperty]
        private bool isEntryEnabled = false;
        [ObservableProperty]
        private bool isBackVisible = false;
        [ObservableProperty]
        private Color buttonDisableColor = Colors.Gray;
        private readonly int durationInSeconds = 0; // Set the duration in seconds

        [ObservableProperty]
        private bool isRunning = false;
        #endregion

        private void StopTimer(bool isLogin)
        {
            if (DisplayTimer != null)
            {
                DisplayTimer.Change(Timeout.Infinite, Timeout.Infinite);
                DisplayTimer.Dispose();
                if (!isLogin)
                {
                    IsBackVisible = true;
                    IsEnabled = false;
                }
            }
        }

        [RelayCommand]
        public void UpdateTimer(object state)
        {
            // Update the seconds
            Seconds--;
            if (Seconds <= durationInSeconds)
            {
                StopTimer(false);
            }
        }

        [RelayCommand]
        public async Task SentOTP()
        {
            IsEntryEnabled = true;
            Seconds = 20;
            DisplayTimer = new Timer(UpdateTimer, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            var selectedCountryValue = SelectCountry.Code;
            //Send OTP to mobile number using API
            //await _sMSService.SendSMS(MobileNumber.ToString(), SelectCountry.Code);

            IsNext = false;
            IsOTP = true;
            var number = MobileNumber;
            DisplayMessage = "An OTP has been sent to your mobile number.";

        }

        [RelayCommand]
        public void Back()
        {
            IsNext = true;
            IsOTP = false;
            IsBackVisible = false;
            DisplayMessage = "Enter your mobile number to proceed.";
            TextDsplayColor = Colors.Black;
        }

        [RelayCommand]
        public async Task MobileLogin()
        {
            IsRunning = true;
            StopTimer(true);

            var selectedCountryValue = SelectCountry;

            //validate OTP from SMS API
            if (OTP.ToString().Length < 6 && OTP.ToString().Length > 6)
            {
                return;
            }

            if (OTP != 123456)
            {
                return;
            }

            Message = "";
            LoginRegisterMobileViewModel loginViewModel = new LoginRegisterMobileViewModel
            {
                MobileNumber = MobileNumber.ToString(),
                OTP = OTP.ToString(),
            };

            var response = await _authService.MobileLoginAsync(loginViewModel);
            if (response != null)
            {
                await LoginOnPostAsync(response);
                IsRunning = false;

                string selectCountryStr = JsonConvert.SerializeObject(SelectCountry);
                Preferences.Set("MobileNumberKey", MobileNumber.ToString());
                Preferences.Set("SelectCountryKey", selectCountryStr);

                var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
                UserDetail userDetail = JsonConvert.DeserializeObject<UserDetail>(userDetailInfoStr);

                if (string.IsNullOrEmpty(userDetail.Name))
                {
                    await NavigationService.NavigateToAsync(nameof(InitialSetUp));
                }
                else
                {
                    await NavigationService.NavigateToAsync(nameof(Home));
                }
            }
        }

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
                await LoginOnPostAsync(response);
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
                                    string language = jsontoken.Claims.FirstOrDefault(f => f.Type == "Language").Value;
                                    string countryCode = jsontoken.Claims.FirstOrDefault(f => f.Type == "CountryCode").Value;
                                    string gender = jsontoken.Claims.FirstOrDefault(f => f.Type == "Gender").Value;
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
                                        Language = language,
                                        CountryCode = countryCode,
                                        Gender = gender

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

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
