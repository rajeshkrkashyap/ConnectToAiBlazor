using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using ConnectToAi.MobileApp.UtilityClasses;
using DataModel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class ResponseCVViewModel : BaseViewModel
    {
        string ssmlResponseText = "";
        bool isSsml = false;
        public ResponseCVViewModel(string responseText, INavigationService navigationService) : base(navigationService)
        {
            if (responseText.StartsWith("<") && responseText.EndsWith(">"))
            {
                isSsml = true;
                ssmlResponseText = responseText;
                ResponseText = ConvertSSMLToText(responseText);
            }
            else
            {
                isSsml = false;
                ResponseText = responseText;
            }

            VolumeOn();
        }

        [ObservableProperty]
        public bool isHighVolumeVisible = true;
        [ObservableProperty]
        public bool isMuteVolumeVisible;
        [ObservableProperty]
        public string responseText;

        string aiAvatarName = "en-US-JennyMultilingualNeural";

        [RelayCommand]
        public async Task VolumeOn()
        {
            var aiAvatarStr = Preferences.Get("AiAvatarName", "");
            var aiAvatar = JsonConvert.DeserializeObject<Avatar>(aiAvatarStr);

            IsHighVolumeVisible = true;
            IsMuteVolumeVisible = false;
            if (ResponseText != null)
            {
                if (aiAvatar==null)
                {
                    aiAvatar = new Avatar() { Country = "USA", Language = "English", Name = "Jenny", Gender = "Female", Value = "en-US-JennyNeural" };
                }
                //var isSSML = Preferences.Get("isSSML", "");
                if (isSsml)
                {
                    await TextToSpeachServices.SsmlToSpeachSynthesizer(ssmlResponseText, aiAvatar.Value, false);
                }
                else
                {
                    await TextToSpeachServices.TextToSpeachSynthesizer(ResponseText, aiAvatar.Value, false);
                }
                IsHighVolumeVisible = false;
                IsMuteVolumeVisible = true;
            }
        }

        [RelayCommand]
        public async Task VolumeOff()
        {
            var aiAvatarStr = Preferences.Get("AiAvatarName", "");
            var aiAvatar = JsonConvert.DeserializeObject<Avatar>(aiAvatarStr);
            IsHighVolumeVisible = true;
            IsMuteVolumeVisible = false;
            if (aiAvatar == null)
            {
                aiAvatar = new Avatar() { Country = "USA", Language = "English", Name = "Jenny", Gender = "Female", Value = "en-US-JennyNeural" };
            }
            if (isSsml)
            {
                await TextToSpeachServices.SsmlToSpeachSynthesizer(ssmlResponseText, aiAvatar.Value, true);
            }
            else
            {
                await TextToSpeachServices.TextToSpeachSynthesizer(ResponseText, aiAvatar.Value, true);
            }


            IsHighVolumeVisible = false;
            IsMuteVolumeVisible = true;
        }

        static string ConvertSSMLToText(string ssmlInput)
        {
            try
            {
                // Load the SSML content into an XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(ssmlInput);

                // Extract text content from the XML document
                XmlNodeList textNodes = xmlDoc.SelectNodes("//text()");
                string plainText = string.Join(" ", textNodes?.Cast<XmlNode>().Select(node => node.InnerText).ToArray());

                return plainText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting SSML to text: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
