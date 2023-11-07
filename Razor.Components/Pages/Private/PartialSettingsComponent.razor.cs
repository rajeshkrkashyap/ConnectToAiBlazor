using DataModel.Entities;
using DataModel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Services;

namespace Razor.Components.Pages.Private
{
    public partial class PartialSettingsComponent
    {
        #region Page Load
        private string SubjectValue
        {
            get
            {
                return _subjectValue;
            }

            set
            {
                _subjectValue = value;
                if (SubjectList != null)
                {
                    var subject = SubjectList.FirstOrDefault(s => s.Id == _subjectValue);
                    SetSubTopics(_subjectValue);
                    HelperText = "Please select a " + subject.Name.ToLower() + " topic!";
                    SetUserSetting("subject", subject.Name);
                }
            }
        }
        private List<Subject>? SubjectList { get; set; }
        public string HelperText { get; set; } = "Please select a english topic!";
        private async void SetSubTopics(string subjectId)
        {
            if (InstructionList != null)
            {
                InstructionList.Clear();
            }

            InstructionService instructionService = new InstructionService(appSettings);
            InstructionList = new Dictionary<string, string>();
            InstructionList = await instructionService.ParentChildrenAsync(subjectId);
            StateHasChanged();
        }

        string _instructionValue = "Please select a topic";
        private string instructionValue
        {
            get
            {
                return _instructionValue;
            }

            set
            {
                _instructionValue = value;
                SetUserSetting("topic", value);
            }
        }
        private Dictionary<string, string>? InstructionList { get; set; }

        async void SetUserSetting(string settingtype, string value)
        {
            AppSettingCookie? appSettingCookie = await GetAppSettingCookie();
            GetSetUserSettings(settingtype, value, appSettingCookie, appSettings);
        }

        private async Task<AppSettingCookie?> GetAppSettingCookie()
        {
            string appUserSettingInfoStr = await JS.InvokeAsync<string>("connectToAiApp.getCookieValue", "cookie_AppUserSetting");
            var appSettingCookie = JsonConvert.DeserializeObject<AppSettingCookie>(appUserSettingInfoStr);
            return appSettingCookie;
        }

        #endregion

        #region JAVA SCRIPT Library
 
        private IJSObjectReference? module1;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                string subjectListStr = await JS.InvokeAsync<string>("connectToAiApp.getCookieValue", "cookie_Subjects");
                if (!string.IsNullOrWhiteSpace(subjectListStr))
                {
                    SubjectList = JsonConvert.DeserializeObject<List<Subject>>(subjectListStr);
                }

                dataService.AppSettingCookie = await GetAppSettingCookie();
                AppUserSettingService appUserSettingService = new AppUserSettingService(appSettings);
                AppUserSetting appUserSetting = await appUserSettingService.GetById(dataService.AppSettingCookie.Id); // get from database
                if (!string.IsNullOrEmpty(appUserSetting.AllSettingsInJSON))
                {
                    var userSetting = JsonConvert.DeserializeObject<UserSetting>(appUserSetting.AllSettingsInJSON);
                    if (userSetting != null)
                    {
                        if (!string.IsNullOrWhiteSpace(userSetting.Subject))
                        {
                            _subjectValue = userSetting.Subject;
                            var subject = SubjectList.FirstOrDefault(s => s.Name == _subjectValue);
                            SetSubTopics(subject.Id);
                        }
                        if (!string.IsNullOrWhiteSpace(userSetting.TopicTitle))
                            _instructionValue = userSetting.TopicTitle;
                    }
                }
                StateHasChanged();
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (module1 is not null)
            {
                await module1.DisposeAsync();
            }
        }
        #endregion
    }
}