using DataModel;
using DataModel.Entities;
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
    public class AppUserService : BaseService
    {
        public AppUserService(AppSettings appSettings) : base(appSettings)
        {
        }

        public async Task<List<AppUser>> ListAsync()
        {
            var returnResponse = new List<AppUser>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserList}";

                var serializedStr = JsonConvert.SerializeObject("");

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<AppUser>>(contentStr);
                }
            }
            return returnResponse;
        }

        public async Task<AppUser> GetById(string id)
        {
            var returnResponse = new AppUser();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserGetById}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<AppUser>(contentStr);
                }
            }
            return returnResponse;
        }

        public async Task<AppUser> UpdateToken(AppUserViewModel appUserViewModel)
        {
            var returnResponse = new AppUser();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.AppUserUpdateTokens}";
                var serializedStr = JsonConvert.SerializeObject(appUserViewModel);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<AppUser>(contentStr);
                }
            }
            return returnResponse;
        }


    }
}
