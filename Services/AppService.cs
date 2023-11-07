using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Services
{
    public class AppService : BaseService
    {
        public AppService(AppSettings appSettings) : base(appSettings)
        {
        }
        public async Task<UserDetail> RefreshToken(UserDetail userDetail, string apiBaseUrl)
        {
            using (var client = new HttpClient())
            {
                var url = $"{apiBaseUrl}{ApiUrl.RefreshToken}";

                var serializedStr = JsonConvert.SerializeObject(new AuthenticationResponse
                {
                    RefreshToken = userDetail.RefreshToken,
                    AccessToken = userDetail.AccessToken
                });

                try
                {
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        var userManagerResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
                        if (userManagerResponse != null && userManagerResponse.IsSuccess)
                        {
                            var tokenDetails = JsonConvert.DeserializeObject<AuthenticationResponse>(userManagerResponse.Content.ToString());

                            userDetail.AccessToken = tokenDetails.AccessToken;
                            userDetail.RefreshToken = tokenDetails.RefreshToken;
                            //await SecureStorage.SetAsync(nameof(Setting.UserDetail), userDetailsStr);
                            return userDetail;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return userDetail;
        }
    }
}
