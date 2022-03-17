using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XandaApp.Infra.Services
{
    public class AppServices
    {
        private static ILogger<AppServices> _logger;

        public AppServices(ILogger<AppServices> logger)
        {
            _logger = logger;
        }

        public static IConfiguration GetConfiguration()
        {
            IConfiguration config;

            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "xandadocs");
                var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false);
                config = builder.Build();
                return config;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in reading configuration file: {ex}");
                return null;
            }
        }

        
    }
}
