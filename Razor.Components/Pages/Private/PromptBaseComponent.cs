using DataModel;
using DataModel.Entities;
using DataModel.Models;
using DataModel.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Services;
using System.Reflection;
using System.Text;


namespace Razor.Components.Pages.Private
{
    public class PromptBaseComponent : ComponentBase
    {
        protected string AddAnimation = AnimationName.FadeDown;
        protected string Duration = "4000";
        protected string Delay = "50";
        public string MinHeight { get; set; } = "750px";
        public string MinPaperHeight { get; set; } = "0px";

        protected string subjectId = "";
        protected string _subjectValue = "English";
        public UserDetail UserDetail { get; set; }
        public AppSettingCookie AppUserSettings { get; set; }

        protected readonly ClientPromptInput ClientPromptInput;
        public PromptBaseComponent()
        {
            ClientPromptInput = new ClientPromptInput();
        }
        protected async void GetSetUserSettings(string settingtype, string value, AppSettingCookie? appSettingCookie, AppSettings appSettings)
        {
            AppUserSettingService appUserSettingService = new AppUserSettingService(appSettings);
            AppUserSetting appUserSetting = await appUserSettingService.GetById(appSettingCookie.Id); // get from database
            UserSetting userSetting = new UserSetting();

            if (!string.IsNullOrEmpty(appUserSetting.AllSettingsInJSON))
            {
                userSetting = JsonConvert.DeserializeObject<UserSetting>(appUserSetting.AllSettingsInJSON);
            }

            if (userSetting != null)
            {
                userSetting.AppUserId = appSettingCookie.AppUserId;
                userSetting.IsQueryActive = appSettingCookie.IsQueryActive;

                switch (settingtype.ToLower())
                {
                    case "speaker":
                        userSetting.IsSpeakerEnabled = value;
                        break;
                    case "save":
                        userSetting.IsSaveEnabled = value;
                        break;
                    case "subject":
                        userSetting.Subject = value;
                        break;
                    case "language":
                        userSetting.Language = value;
                        break;
                    case "topic":
                        InstructionService instructionService = new InstructionService(appSettings); //try to remove this call 
                        var instruction = await instructionService.GetBySubjectIdAndTitle(value, _subjectValue);  // and get id from the UI/topic dropdown
                        userSetting.TopicTitle = instruction.Title;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(userSetting.Subject))
                {
                    //userSetting.Subject = "English";
                }

            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Update into database
            var userSettingJson = JsonConvert.SerializeObject(userSetting, Formatting.None, settings);
            await appUserSettingService.UpdateByColumnName("AllSettingsInJSON", userSettingJson, appSettingCookie.Id);


        }

        protected async Task<string> UpdateUiWithOutStream(HttpResponseMessage? response, string userPrompt)
        {
            try
            {
                if (response != null && response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new System.IO.StreamReader(responseStream))
                        {
                            var mainResponse = await reader.ReadToEndAsync();
                            var chatCompletion = System.Text.Json.JsonSerializer.Deserialize<ChatCompletion>(mainResponse);
                            if (chatCompletion != null && chatCompletion.choices.Count() > 0)
                            {
                                var content = chatCompletion.choices[0].message.content;
                                ////var altTexts = ProcessResponseContent(content);
                                //ResponseResult responseResult = new()
                                //{
                                //    UserPrompt = userPromt,
                                //    AltTexts = altTexts,
                                //    Content = content,
                                //};
                                //return JsonConvert.SerializeObject(responseResult);
                                return content;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return "failed";
        }
        protected async Task<HttpResponseMessage?> ProcessPrompt(AppSettings appSettings, ClientPromptInput clientPromptInput)
        {
            using (var client = new HttpClient())
            {
                var url = $"{appSettings.ApiBaseUrl}{ApiUrl.GptPropcessPrompt}";
                var serializedStr = JsonConvert.SerializeObject(clientPromptInput);
                return await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
            }
        }
    }
}
