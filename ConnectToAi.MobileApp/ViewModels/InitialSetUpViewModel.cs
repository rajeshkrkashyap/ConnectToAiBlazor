using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.Pages;
using DataModel;
using DataModel.Entities;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using Services;
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;


namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class InitialSetUpViewModel : BaseViewModel
    {
        private readonly AppUserService _appUserService;
        public InitialSetUpViewModel(Country country, Int64 mobileNumber, AppSettings appSettings, INavigationService navigationService) : base(navigationService)
        {
            SelectedCountry = country;
            MobileNumber = mobileNumber;
            _appUserService = new AppUserService(appSettings);
            PopulatLanguages();
            PopulatGender();
        }


        [ObservableProperty]
        private string displayMessage = "Enter your details to proceed.";

        #region Form Fields
        [ObservableProperty]
        private Int64 mobileNumber;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string selectedGender;
        [ObservableProperty]
        private string selectedLanguage;
        [ObservableProperty]
        private Country selectedCountry;
        #endregion

        #region Page Properties
        [ObservableProperty]
        private ObservableCollection<string> languages;
        [ObservableProperty]
        private ObservableCollection<string> genders;
        #endregion


        [RelayCommand]
        public async Task Update()
        {
            AppUser appUser = new AppUser();
            appUser.Name = Name;
            appUser.PhoneNumber = MobileNumber.ToString();
            appUser.Gender = SelectedGender;
            appUser.CountryCode = SelectedCountry.Code;

            var response = await _appUserService.Update(appUser);
            if (response != null)
            {
                var userDetailInfoStr= Preferences.Get("UserLoggedInKey", "");
                var userDetailPrevious = JsonConvert.DeserializeObject<UserDetail>(userDetailInfoStr);
                var userDetail = new UserDetail
                {
                    Email = response.Email,
                    Name = response.Name,
                    Role = userDetailPrevious.Role,
                    AccessToken = userDetailPrevious.AccessToken,
                    RefreshToken = userDetailPrevious.RefreshToken,
                    UserAvatar = !string.IsNullOrWhiteSpace(response.UserAvatar) ? $"{ApiUrl.ApiBaseURL}/{response.UserAvatar}" : "",
                    UserID = response.Id,
                    Tokens = Convert.ToDecimal(response.BalanceToken),
                    Language = response.Language,
                    CountryCode = response.CountryCode,
                    Gender = response.Gender
                };
                userDetailInfoStr = JsonConvert.SerializeObject(userDetail);
                Preferences.Set("UserLoggedInKey", userDetailInfoStr);

                await NavigationService.NavigateToAsync(nameof(Home));
            }
        }
        public void PopulatLanguages()
        {
            Languages = new ObservableCollection<string>
            {
                "English",
                "Hindi"
            };
            SelectedLanguage = "English";
        }
        public void PopulatGender()
        {
            Genders = new ObservableCollection<string>
            {
                "Male",
                "FeMale",
                "Other"
            };
            SelectedGender = "Male";
        }
    }
}
