using System;
using TeaseAI.Common.Interfaces.Accessors;
using Windows.Storage;

namespace TeaseAI.PersonalityEditor.Services
{
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        public string GetBaseFolder() => ApplicationData.Current.LocalFolder.Path;
    }
}
