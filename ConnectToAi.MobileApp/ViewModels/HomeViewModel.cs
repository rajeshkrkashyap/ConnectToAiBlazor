using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataModel;
using DataModel.Models;
using DataModel.Utility;
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
        public HomeViewModel(AppSettings appSettings)
        {
            _promptService = new PromptService(appSettings);
        }

        public async Task<HttpResponseMessage?> SendPromtCommand(string speachText)
        {
            // Read the file content into a audioStream
            //byte[] audioStreamBytes = new byte[audioStreamPrompt.Length];
            //audioStreamPrompt.Read(audioStreamBytes, 0, (int)audioStreamPrompt.Length);

            // Convert byte array to base64-encoded string
            //string base64Content = Convert.ToBase64String(audioStreamBytes);

            // Create a JSON payload with the base64-encoded file content
            //string jsonPayload = $"{{\"fileContent\": \"{base64Content}\"}}";

            ClientPromptInput clientPromptInput = new ClientPromptInput
            {
                Prompt = speachText,
                InputType = "text",
                UserId = "a1769504-6f90-41b3-afba-3d591f1489f7"
            };

            return await _promptService.PropcessPrompt(clientPromptInput);
        }

    }
}
