using Newtonsoft.Json;
using System;
using System.IO;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.PersonalityEditor.Services
{
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        public ApplicationConfiguration GetApplicationConfiguration()
        {
            var configFile = GetAppSettingsFolder() + Path.DirectorySeparatorChar + "applicationsettings.json";
            if (!File.Exists(configFile))
                return CreateDefaultConfig();
            var data = File.ReadAllText(configFile);
            return JsonConvert.DeserializeObject<ApplicationConfiguration>(data);
        }

        public string GetBaseFolder() => GetApplicationConfiguration().PersonalityHome;

        public Result SaveApplicationConfiguration(ApplicationConfiguration applicationConfiguration)
        {
            try
            {
                var configFile = GetAppSettingsFolder() + Path.DirectorySeparatorChar + "applicationsettings.json";
                var data = JsonConvert.SerializeObject(applicationConfiguration);
                File.WriteAllText(configFile, data);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private ApplicationConfiguration CreateDefaultConfig()
        {
            return new ApplicationConfiguration
            {
                PersonalityHome = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "TeaseAI"
            };
        }

        private string GetAppSettingsFolder()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar + "TeaseAI";
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
