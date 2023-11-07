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
    public class RechargeService : BaseService
    {
        public RechargeService(AppSettings appSettings) : base(appSettings)
        {
        }
        public async Task<List<Recharge>> ListAsync()
        {
            var returnResponse = new List<Recharge>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeList}";

                var serializedStr = JsonConvert.SerializeObject("");

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<Recharge>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<List<Recharge>> GetByUserId(string userId)
        {
            var returnResponse = new List<Recharge>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeGetByUserId}/?userId=" + userId;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<List<Recharge>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Recharge> GetById(string id)
        {
            var returnResponse = new Recharge();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeGetById}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Recharge>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Recharge> GetByRazorpayPaymentId(string razorpay_payment_id)
        {
            var returnResponse = new Recharge();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeGetByRazorpayPaymentId}/?razorpay_payment_id=" + razorpay_payment_id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Recharge>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Recharge> GetByCurrency(string currency)
        {
            var returnResponse = new Recharge();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeGetByCurrency}/?currency=" + currency;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Recharge>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Recharge> CreateAsync(Recharge Recharge)
        {
            var returnResponse = new Recharge();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeCreate}";

                var serializedStr = JsonConvert.SerializeObject(Recharge);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Recharge>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Recharge> Update(string id)
        {
            var returnResponse = new Recharge();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeUpdate}/?id=" + id;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Recharge>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.RechargeDelete}/?id=" + id;
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
