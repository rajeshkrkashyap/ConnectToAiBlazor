using DataModel.Models;
using DataModel.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;

namespace ConnectToAiWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly BlobStorageService _blobStorageService;
        private readonly AppSettings _appSettings;
        public HomeController(IWebHostEnvironment hostingEnvironment, BlobStorageService blobStorageService, AppSettings appSettings)
        {
            _hostingEnvironment = hostingEnvironment;
            _blobStorageService = blobStorageService;
            _appSettings = appSettings;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            OcrService _ocrService = new OcrService(_appSettings);
            ConfigService _configService= new ConfigService(null);
            var isAzureStorage = false;
            if (file != null && file.Length > 0)
            {
                MathOCRResponse _imageContent = null;
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var host = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

                if (!isAzureStorage)
                {
                    try
                    {
                        var uploadPath = _hostingEnvironment.ContentRootPath;
                        _imageContent = await _ocrService.ReadImageData(uploadPath, host, fileName, file, false);
                        //dynamic content = JsonConvert.DeserializeObject<object>(_imageContent);
                        var textContent = _imageContent.text;
                        //var textType = content.data[0].type;
                        //var textvalue = content.data[0].value;

                        var _blobPath = "https://connectto.ai/propmt-image/" + fileName;

                        //return JsonConvert.SerializeObject(new { success = true, blobPath = _blobPath, imageContent = textContent });
                        return Json(new { success = true, blobPath = _blobPath, imageContent = textContent });
                    }
                    catch (Exception ex)
                    {
                        //return JsonConvert.SerializeObject(new { success = false, message = "Error uploading file: " + ex.Message });
                        return Json(new { success = false, message = "Error uploading file: " + ex.Message });
                    }
                }
                else
                {
                    try
                    {
                        _blobStorageService.BlobConnectionString = _configService.AppSettings.BlobConnectionString;
                        _blobStorageService.BlobContainer = _configService.AppSettings.BlobImagesContainer;
                        fileName = _blobStorageService.UploadImages(file, fileName);

                        string _blobPath = "";

                        while (true)
                        {
                            if (string.IsNullOrEmpty(_blobPath))
                            {
                                _blobPath = await _blobStorageService.GetImageAsDataUrlAsync(fileName);
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (_blobPath != null)
                        {
                            _imageContent = await _ocrService.ReadImageData(null, host, fileName, null, true);
                        }
                        //return JsonConvert.SerializeObject(new { success = true, blobPath = _blobPath, imageContent = _imageContent });
                        return Json(new { success = true, blobPath = _blobPath, imageContent = _imageContent.text });
                    }
                    catch (Exception ex)
                    {
                        //return JsonConvert.SerializeObject(new { success = false, message = "Error uploading file: " + ex.Message });
                        return Json(new { success = false, message = "Error uploading file: " + ex.Message });
                    }
                }
            }
            else
            {
                //return JsonConvert.SerializeObject(new { success = false, message = "No file selected" });
                return Json(new { success = false, message = "No file selected" });
            }
        }
    }
}
