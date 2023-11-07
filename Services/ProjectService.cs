using DataModel;
using DataModel.Entities;
using DataModel.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(AppSettings appSettings) : base(appSettings)
        {
        }
        public async Task<List<Project>> ListAsync(string userId)
        {
            var returnResponse = new List<Project>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectList}/?userId=" + userId;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<Project>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Project> GetById(string id)
        {
            var returnResponse = new Project();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectGetById}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Project>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Project> GetByTitle(string name, string appUserId)
        {
            var returnResponse = new Project();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectGetByName}/?name=" + name + "&appUserId=" + appUserId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Project>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> IsTitleExist(string name, string appUserId)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectIsExist}/?name=" + name + "&appUserId=" + appUserId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<string> CreateAsync(Project Project)
        {
            var returnResponse = string.Empty;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectCreate}";

                var serializedStr = JsonConvert.SerializeObject(Project);

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
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectUpdate}/?id=" + id + "&ParentId=" + ParentId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Project> Update(string id)
        {
            var returnResponse = new Project();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectUpdate}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Project>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.ProjectDelete}/?id=" + id;
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
