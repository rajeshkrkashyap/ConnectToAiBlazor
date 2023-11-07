using DataModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseService
    {
        private readonly AppSettings _appSettings;
        public BaseService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        protected AppSettings AppSettings { get { return _appSettings; } }
    }

}
