using DataModel;
using DataModel.Models;
using DataModel.Utility;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Services
{
    public class OcrService : BaseService
    {
        public OcrService(AppSettings appSettings) : base(appSettings)
        {
        }
        static void UploadFileToFtp(IFormFile file, string fileName)
        {
            try
            {
                byte[] buffer;
                using (Stream fs = file.OpenReadStream())
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                }

                string ftpServer = "ftp://win5045.site4now.net/";
                string userName = "propmtuser";
                string password = "Mokshit@123";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(userName, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                }

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        static void UploadFileToFtp(Stream stream, string fileName)
        {
            try
            {
                byte[] buffer;
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                string ftpServer = "ftp://win5045.site4now.net/";
                string userName = "propmtuser";
                string password = "Mokshit@123";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(userName, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                }

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        public async Task<MathOCRResponse> ReadImageData(string uploadPath, string host, string fileName, IFormFile file, bool isStorageAzure)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string imageUrl = "";
                    if (isStorageAzure)
                    {
                        imageUrl = "https://blobconnect.blob.core.windows.net/filecontainer/" + fileName;
                    }
                    else
                    {
                        UploadFileToFtp(file, fileName);
                        imageUrl = "https://connectto.ai/propmt-image/" + fileName;
                    }

                    MathOCRResponse returnResponse = null;
                    using (var client = new HttpClient())
                    {
                        var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.OCRReadImageData}/?imageUrl=" + imageUrl;

                        //var serializedStr = JsonConvert.SerializeObject(new { imageUrl = imageUrl });
                        //var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                        var response = await client.PostAsync(url, null);

                        if (response.IsSuccessStatusCode)
                        {
                            string contentStr = await response.Content.ReadAsStringAsync();
                            returnResponse = JsonConvert.DeserializeObject<MathOCRResponse>(contentStr);
                        }
                    }
                    return returnResponse;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }
        public async Task<MathOCRResponse> ReadImageData(string fileName, Stream stream, bool isStorageAzure)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string imageUrl = "";
                    if (isStorageAzure)
                    {
                        imageUrl = "https://blobconnect.blob.core.windows.net/images/" + fileName;
                    }
                    else
                    {
                        UploadFileToFtp(stream, fileName);
                        imageUrl = "https://connectto.ai/propmt-image/" + fileName;
                    }

                    MathOCRResponse returnResponse = null;
                    using (var client = new HttpClient())
                    {
                        var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.OCRReadImageData}/?imageUrl=" + imageUrl;

                        //var serializedStr = JsonConvert.SerializeObject(new { imageUrl = imageUrl });
                        //var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                        var response = await client.PostAsync(url, null);

                        if (response.IsSuccessStatusCode)
                        {
                            string contentStr = await response.Content.ReadAsStringAsync();
                            returnResponse = JsonConvert.DeserializeObject<MathOCRResponse>(contentStr);
                        }
                    }
                    return returnResponse;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }
    }


}
