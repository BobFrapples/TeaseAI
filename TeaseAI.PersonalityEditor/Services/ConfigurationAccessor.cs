using System;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.PersonalityEditor.Services
{
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        public string GetBaseFolder() => @"C:\source\Tease-AI.Data";
    }
}
