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
    public class TopicMediaService : BaseService
    {
        public TopicMediaService(AppSettings appSettings) : base(appSettings)
        {
        }
        public async Task<List<TopicMedia>> ListAsync()
        {
            var returnResponse = new List<TopicMedia>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaList}";

                var serializedStr = JsonConvert.SerializeObject("");

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<TopicMedia>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<TopicMedia> GetById(string id)
        {
            var returnResponse = new TopicMedia();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaGetById}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<TopicMedia>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<List<TopicMedia>> GetListByTopicId(string id)
        {
            var returnResponse = new List<TopicMedia>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaGetListByTopicId}/?topicId=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<TopicMedia>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<TopicMedia> CreateAsync(TopicMedia TopicMedia)
        {
            var returnResponse = new TopicMedia();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaCreate}";

                var serializedStr = JsonConvert.SerializeObject(TopicMedia);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<TopicMedia>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> BuklCreateAsync(List<TopicMedia> topicMedias)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaBulkCreate}";

                var serializedStr = JsonConvert.SerializeObject(topicMedias);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<TopicMedia> Update(string id)
        {
            var returnResponse = new TopicMedia();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaUpdate}";

                var serializedStr = JsonConvert.SerializeObject(id);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<TopicMedia>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.TopicMediaDelete}";

                var serializedStr = JsonConvert.SerializeObject(id);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

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
