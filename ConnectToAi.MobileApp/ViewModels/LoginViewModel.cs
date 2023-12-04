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
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
            PopulatCountries();
        }

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isNext = true;
        [ObservableProperty]
        private bool isOTP;

        [ObservableProperty]
        private string displayMessage = "Enter your mobile number to proceed.";

        #region Form fields
        [ObservableProperty]
        private int mobileNumber;
        [ObservableProperty]
        private int oTP;
        [ObservableProperty]
        private Country selectCountry;
        #endregion

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

        //[RelayCommand]
        //public void MobileText()
        //{
        //    IsEnabled = true;
        //    ButtonDisableColor = Colors.Green;
        //}

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
        public void SentOTP()
        {
            IsEntryEnabled = true;
            Seconds = 20;
            DisplayTimer = new Timer(UpdateTimer, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            //Send OTP to mobile number using API

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
            StopTimer(true);
            var number = MobileNumber;
            var otp = OTP;
            var selectedCountryValue = SelectCountry;

            //validate OTP from SMS API

            Message = "";
            LoginRegisterMobileViewModel loginViewModel = new LoginRegisterMobileViewModel
            {
                MobileNumber = MobileNumber.ToString(),
                OTP = otp.ToString(),
            };
            var response = await _authService.MobileLoginAsync(loginViewModel);
            if (response != null)
            {
                await LoginOnPostAsync(response);
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

        [ObservableProperty]
        private ObservableCollection<Country> countries;

        public void PopulatCountries()
        {
            Countries = new ObservableCollection<Country>
            {
                new Country() { Code = "001", Name = "USA" },
                new Country() { Code = "001", Name = "CAN" },
                new Country() { Code = "044", Name = "UK" },
                new Country() { Code = "061", Name = "AUS" },
                new Country() { Code = "049", Name = "GER" },
                new Country() { Code = "033", Name = "FRA" },
                new Country() { Code = "081", Name = "JPN" },
                new Country() { Code = "055", Name = "BRA" },
                new Country() { Code = "091", Name = "IND" },
                new Country() { Code = "027", Name = "SAF" },
                new Country() { Code = "086", Name = "CHN" },
                new Country() { Code = "007", Name = "RUS" },
                new Country() { Code = "034", Name = "ESP" },
                new Country() { Code = "039", Name = "ITA" },
                new Country() { Code = "052", Name = "MEX" },
                new Country() { Code = "082", Name = "KOR" },
                new Country() { Code = "054", Name = "ARG" },
                new Country() { Code = "234", Name = "NGA" },
                new Country() { Code = "020", Name = "EGY" },
                new Country() { Code = "254", Name = "KEN" },
                new Country() { Code = "966", Name = "SAU" },
                new Country() { Code = "090", Name = "TUR" },
                new Country() { Code = "066", Name = "THA" },
                new Country() { Code = "084", Name = "VNM" },
                new Country() { Code = "062", Name = "IDN" },
                new Country() { Code = "031", Name = "NLD" },
                new Country() { Code = "030", Name = "GRC" },
                new Country() { Code = "046", Name = "SWE" },
                new Country() { Code = "047", Name = "NOR" },
                new Country() { Code = "051", Name = "PER" },
                new Country() { Code = "351", Name = "PRT" },
                new Country() { Code = "048", Name = "POL" },
                new Country() { Code = "353", Name = "IRL" },
                new Country() { Code = "041", Name = "CHE" },
                new Country() { Code = "043", Name = "AUT" },
                new Country() { Code = "032", Name = "BEL" },
                new Country() { Code = "045", Name = "DNK" },
                new Country() { Code = "358", Name = "FIN" },
                new Country() { Code = "064", Name = "NZL" },
                new Country() { Code = "065", Name = "SGP" },
                new Country() { Code = "060", Name = "MYS" },
                new Country() { Code = "063", Name = "PHL" },
                new Country() { Code = "098", Name = "IRN" },
                new Country() { Code = "096", Name = "IRQ" },
                new Country() { Code = "972", Name = "ISR" },
                new Country() { Code = "961", Name = "LBN" },
                new Country() { Code = "211", Name = "SSD" },
                new Country() { Code = "977", Name = "NPL" },
                new Country() { Code = "092", Name = "PAK" },
                new Country() { Code = "880", Name = "BGD" },
                new Country() { Code = "094", Name = "LKA" },
                new Country() { Code = "420", Name = "CZE" },
                new Country() { Code = "036", Name = "HUN" },
                new Country() { Code = "385", Name = "HRV" },
                new Country() { Code = "040", Name = "ROU" },
                new Country() { Code = "359", Name = "BGR" },
                new Country() { Code = "372", Name = "EST" },
                new Country() { Code = "371", Name = "LVA" },
                new Country() { Code = "370", Name = "LTU" },
                new Country() { Code = "381", Name = "SRB" }
            };
            SelectCountry = Countries.First(c => c.Name == "IND");
        }

    }

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
