using DataModel.Models;
using DataModel;
using DataModel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Entities;

namespace Services
{
    public class SMSService : BaseService
    {
        public SMSService(AppSettings appSettings) : base(appSettings)
        {

        }

        public async Task SendSMS(string mobileNumber, string countryCode)
        {
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.SMSSend}/?mobileNumber=" + mobileNumber + "&countryCode=" + countryCode;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    var returnResponse = JsonConvert.DeserializeObject(contentStr);
                }
            }
        }
    }
}
