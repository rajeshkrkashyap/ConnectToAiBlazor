using DataModel;
using DataModel.Entities;
using DataModel.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Services
{
    public class AppUserSettingService : BaseService
    {
        public AppUserSettingService(AppSettings appSettings) : base(appSettings) { }
        public async Task<List<AppUserSetting>> ListByAppUserIdAsync(string id)
        {
            var returnResponse = new List<AppUserSetting>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingList}/?id=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<AppUserSetting>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<AppUserSetting> GetAppSettingByAppUserIdAsync(string id)
        {
            var returnResponse = new AppUserSetting();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppSettingQueryActiveByAppUserIdAsync}/?id=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<AppUserSetting>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<AppUserSetting> GetById(string id)
        {
            var returnResponse = new AppUserSetting();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingGetById}/?id=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<AppUserSetting>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<AppUserSetting> CreateAsync(AppUserSetting AppUserSetting)
        {
            var returnResponse = new AppUserSetting();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingCreate}";

                var serializedStr = JsonConvert.SerializeObject(AppUserSetting);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<AppUserSetting>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Update(AppUserSetting appUserSetting)
        {
            bool returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingUpdate}";

                var serializedStr = JsonConvert.SerializeObject(appUserSetting);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> UpdateByColumnName(string column, string value, string id)
        {
            bool returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingUpdateByColumnName}/?column=" + column + "&value=" + value + "&id=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> ActivateQuery(AppUserSetting appUserSetting)
        {
            bool returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingActivateQuery}";

                var serializedStr = JsonConvert.SerializeObject(appUserSetting);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> SetInputType(AppUserSetting appUserSetting)
        {
            bool returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingSetInputType}";

                var serializedStr = JsonConvert.SerializeObject(appUserSetting);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserSettingDelete}";

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
