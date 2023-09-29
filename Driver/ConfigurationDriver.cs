using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TeamsWindowsApp.Driver
{
    public class ConfigurationDriver
    {
        private readonly Lazy<IConfiguration> _configurationLazy;
        private const string SeleniumBaseUrlConfigFieldName = "baseUrl";

        public ConfigurationDriver()
        {
            _configurationLazy = new Lazy<IConfiguration>(GetConfiguration);
        }

        public IConfiguration Configuration => _configurationLazy.Value;
        public string baseUrl => Configuration[SeleniumBaseUrlConfigFieldName];

        private IConfiguration GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            string directoryName = Path.GetDirectoryName(typeof(ConfigurationDriver).Assembly.Location);
            configurationBuilder.AddJsonFile(Path.Combine(directoryName, @"appsettings.json"));
            
            return configurationBuilder.Build();
        }
    }
}
