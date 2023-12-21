using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PromptService : BaseService
    {
        public PromptService(AppSettings appSettings) : base(appSettings)
        {
        }

        public async Task<HttpResponseMessage?> PropcessTalkingAvatarPrompt(ClientPromptInput clientPromptInput)
        {
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.GptPropcessTalkingAvatarPrompt}";
                var serializedStr = JsonConvert.SerializeObject(clientPromptInput);
                return await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
            }
        }

        public async Task<HttpResponseMessage?> PropcessPrompt(ClientPromptInput clientPromptInput)
        {
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.GptPropcessPrompt}";
                var serializedStr = JsonConvert.SerializeObject(clientPromptInput);
                return await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
            }
        }
        public async Task<HttpResponseMessage?> PropcessImagePrompt(ImagePrompt clientImagePrompt)
        {
            using (var client = new HttpClient())
            {
                string url = $"{AppSettings.ApiBaseUrl}{ApiUrl.GptPropcessImagePrompt}";
                var serializedStr = JsonConvert.SerializeObject(clientImagePrompt);
                return await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
            }
        }
    }
}
