using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class DataService
    {
        public DataService()
        {
            OrderViewModel = new OrderViewModel();
            AppSettingCookie = new AppSettingCookie();
            PromptInputSettings = new ClientPromptInput();
        }
        public OrderViewModel OrderViewModel { get; set; }
        public AppSettingCookie AppSettingCookie { get; set; }
        public ClientPromptInput PromptInputSettings { get; set; }
    }
}
