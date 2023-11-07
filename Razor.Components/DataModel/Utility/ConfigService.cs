using Microsoft.Extensions.Options;

namespace Razor.Components.DataModel.Utility
{
    public class ConfigService
    {
        IOptions<AppSettings> _options;
        public ConfigService(IOptions<AppSettings> options)
        {
            _options = options;
        }

        private AppSettings? settings;
        public async Task LoadDataAsync(string appPath)
        {
            settings = _options.Value;
            settings.AppRootFolderpath = appPath;
        }

        public AppSettings? AppSettings
        {
            get
            {
                return _options.Value;
            }
        }
    }
}
