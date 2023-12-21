using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using DataModel;
using DataModel.Entities;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly PromptService _promptService;
        public HomeViewModel(AppSettings appSettings, INavigationService navigationService) : base(navigationService)
        {
            _promptService = new PromptService(appSettings);
        }

        public async Task<HttpResponseMessage?> SendPromtCommand(string speachText)
        {
            var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
            var userDetail = JsonConvert.DeserializeObject<UserDetail>(userDetailInfoStr);

            string instructionId = "C8CF1F0C-024A-4194-A73A-88910808CD59";
            if (userDetail.Language == "Talking Avatar")
            {
                instructionId = "C8CF1F0C-024A-4194-A73A-88910808CD59";
            }
            else
            {
                instructionId = "C8CF1F0C-024A-4194-A73A-88910808CD59";
            }
            Avatar aiAvatar = null;
            var aiAvatarStr = Preferences.Get("AiAvatarName", "");
            if (string.IsNullOrEmpty(aiAvatarStr))
            {
                aiAvatar = new Avatar() { Country = "USA", Language = "English", Name = "Jenny", Gender = "Female", Value = "en-US-JennyNeural" };
            }
            else
            {
                aiAvatar = JsonConvert.DeserializeObject<Avatar>(aiAvatarStr);
            }

            ClientPromptInput clientPromptInput = new ClientPromptInput
            {
                Prompt = speachText,
                InputType = "text",
                UserId = userDetail.UserID,
                Name = userDetail.Name,
                InstructionId = instructionId,
                Language = aiAvatar.Language
            };

            return await _promptService.PropcessTalkingAvatarPrompt(clientPromptInput);
        }

    }
}
