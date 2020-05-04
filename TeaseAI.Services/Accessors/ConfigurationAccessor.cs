using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.IO;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
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

        public string GetBaseFolder() => GetApplicationConfiguration().BaseDataFolder;

        public Result SaveApplicationConfiguration(ApplicationConfiguration applicationConfiguration)
        {
            try
            {
                var configFile = GetAppSettingsFolder() + Path.DirectorySeparatorChar + "applicationsettings.json";
                var data = JsonConvert.SerializeObject(applicationConfiguration, Formatting.Indented);
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
                BaseDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "TeaseAI"
            };
        }

        public string GetDatabaseConnectionString()
        {
            var dbFile = GetAppSettingsFolder() + Path.DirectorySeparatorChar + "teaseai-database.sqlite";
            if (!File.Exists(dbFile))
                File.Copy("default.sqlite", dbFile);

            return "Data Source=" + dbFile + ";Version=3;";
        }

        public string GetSettingsLocation() => GetAppSettingsFolder() + Path.DirectorySeparatorChar + "settings.json";

        private string GetAppSettingsFolder()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar + "TeaseAI";
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
