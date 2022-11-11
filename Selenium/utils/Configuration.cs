using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Selenium.utils
{
    public class Configuration
    {
        public JObject GetConfig()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                                 Microsoft.Extensions.Hosting.Environments.Development;
            string filePath = Environment.CurrentDirectory + $"/launchSettings.{environment}.json";
            return JObject.Parse(File.ReadAllText(filePath));
        }
    }
}