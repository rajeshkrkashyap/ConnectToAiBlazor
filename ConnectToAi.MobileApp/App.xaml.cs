using ConnectToAi.MobileApp.ViewModels;
using System.Collections.ObjectModel;
using System.Net;

namespace ConnectToAi.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZiW35ecH1XR2RfVER0XA==");
            MainPage = new AppShell();
            PopulatAiAvatars();
        }

        
        public static Avatar selectAiAvatar;
        
        public static ObservableCollection<Avatar> AiAvatars;

        public Avatar SelectAiAvatar { get;  set; }
        public static string CookieStr { get; set; }
        public static string[] CookieContainer { get; set; }

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
    }
}
