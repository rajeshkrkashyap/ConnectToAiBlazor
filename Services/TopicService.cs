using DataModel;
using DataModel.Entities;
using DataModel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TopicService : BaseService
    {
        public TopicService(AppSettings appSettings) : base(appSettings)
        {
        }
        public async Task<List<Topic>> ListAsync(string subjectId, string userId)
        {
            var returnResponse = new List<Topic>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicList}/?subjectId=" + subjectId + "&userId=" + userId;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<Topic>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Topic> GetById(string id)
        {
            var returnResponse = new Topic();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicGetById}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Topic>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Topic> GetByTitle(string title, string appUserId)
        {
            var returnResponse = new Topic();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicGetByTitle}/?title=" + title + "&appUserId=" + appUserId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Topic>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> IsTitleExist(string title, string appUserId)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicIsTitleExist}/?title=" + title + "&appUserId=" + appUserId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<string> CreateAsync(Topic Topic)
        {
            var returnResponse = string.Empty;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicCreate}";

                var serializedStr = JsonConvert.SerializeObject(Topic);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    returnResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return returnResponse;
        }
        public async Task<bool> UpdateParent(string id, string ParentId)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicUpdate}/?id=" + id + "&ParentId=" + ParentId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Topic> Update(string id)
        {
            var returnResponse = new Topic();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicUpdate}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Topic>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicDelete}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
    }
}
