using System;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.Accessors;

namespace TeaseAI.Services
{
    public static class ServiceFactory
    {
        public static IPersonalityService CreatePersonalityService(string baseFolder)
        {
            return new PersonalityService(baseFolder);
        }

        public static IScriptAccessor CreateScriptService(string baseFolder)
        {
            return new ScriptAccessor(baseFolder, CreateCldAccessor());
        }

        private static ICldAccessor CreateCldAccessor()
        {
            return new CldAccessor();
        }
    }
}
