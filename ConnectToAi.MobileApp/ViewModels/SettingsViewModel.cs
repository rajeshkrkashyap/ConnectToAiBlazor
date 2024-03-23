using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectToAi.MobileApp.Navigation;
using DataModel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToAi.MobileApp.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(INavigationService navigationService) : base(navigationService)
        {
            PopulatAiAvatars();
            PopulatCountries();
            var aiAvatarStr = Preferences.Get("AiAvatarName", "");
            if (string.IsNullOrEmpty(aiAvatarStr))
            {
                var aiAvatar = JsonConvert.DeserializeObject<Avatar>(aiAvatarStr);
                SelectAiAvatar = aiAvatar;
            }

        }

        [ObservableProperty]
        private Avatar selectAiAvatar;
        [ObservableProperty]
        private ObservableCollection<Avatar> aiAvatars;

        public void PopulatAiAvatars()
        {
            AiAvatars = new ObservableCollection<Avatar>
            {
                new Avatar() { Country="USA",Language="English",Name = "Jenny", Gender ="Female", Value= "en-US-JennyNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Aria", Gender ="Female", Value= "en-US-AriaNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Amber", Gender ="Female", Value= "en-US-AmberNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Ana", Gender ="Female", Value= "en-US-AnaNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Ashley", Gender ="Female", Value= "en-US-AshleyNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Cora", Gender ="Female", Value= "en-US-CoraNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Elizabeth", Gender ="Female", Value= "en-US-ElizabethNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Emma", Gender ="Female", Value= "en-US-EmmaNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Eric", Gender ="Female", Value= "en-US-EricNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Jane", Gender ="Female", Value= "en-US-JaneNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Michell", Gender ="Female", Value= "en-US-MichelleNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Monica", Gender ="Female", Value= "en-US-MonicaNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Nancy", Gender ="Female", Value= "en-US-NancyNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Sara", Gender ="Female", Value= "en-US-SaraNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Guy", Gender ="Male", Value= "en-US-GuyNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Davi", Gender ="Male", Value= "en-US-DavisNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Andrew", Gender ="Male", Value= "en-US-AndrewNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Jacob", Gender ="Male", Value= "en-US-JacobNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Brandon", Gender ="Male", Value= "en-US-BrandonNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Brain", Gender ="Male", Value= "en-US-BrianNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Cristopher", Gender ="Male", Value= "en-US-ChristopherNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Jason", Gender ="Male", Value= "en-US-JasonNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Roger", Gender ="Male", Value= "en-US-RogerNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Steffan", Gender ="Male", Value= "en-US-SteffanNeural"  },
                new Avatar() { Country="USA",Language="English",Name = "Tony", Gender ="Male", Value= "en-US-TonyNeural"  },

                new Avatar() { Country="India",Language="English",Name = "Neerja", Gender ="Female", Value= "en-IN-NeerjaNeural"  },
                new Avatar() { Country="India",Language="English",Name = "Prabhat", Gender ="Male", Value= "en-IN-PrabhatNeural"  },
                new Avatar() { Country="India",Language="Hindi",Name = "Swara", Gender ="Female", Value= "hi-IN-SwaraNeural"  },
                new Avatar() { Country="India",Language="Hindi",Name = "Madhur", Gender ="Male", Value= "hi-IN-MadhurNeural"  },
            };

            SelectAiAvatar = AiAvatars.First(c => c.Name == "Jenny");
        }

        [RelayCommand]
        public void LogOut()
        {
            Preferences.Set("UserLoggedInKey", "");
            var userDetailInfoStr = Preferences.Get("UserLoggedInKey", "");
            Preferences.Remove("UserLoggedInKey");
            Preferences.Clear();
            NavigationService.NavigateToAsync(nameof(LoginPage));
        }
    }

    public class Avatars
    {
        private readonly List<Avatar> _avatarList;
        public Avatars()
        {
            _avatarList = new List<Avatar>();
            CreateAvatarList();
        }
        public List<Avatar> AvatarList { get { return _avatarList; } }
        public void CreateAvatarList()
        {
            AvatarList.Add(new Avatar()
            {
                Name = "Jenny",
                Value = "JennyNeural",
                Gender = "Female",
            });
        }
    }
    public class Avatar
    {
        public string Country { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Gender { get; set; }
    }
    public class SeapingStyle
    {
        private readonly string _style;
        public SeapingStyle(string style)
        {
            _style = style;
        }
        public string Style
        {
            get
            {
                if (string.IsNullOrEmpty(_style))
                {
                    return "Soft";
                }
                return _style;
            }
        }
    }
}
