using System.IO;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class PathsAccessor : IPathsAccessor
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public PathsAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public string RiskyPickScript => throw new System.NotImplementedException();

        public string GetPersonalitiesFolder() =>
            _configurationAccessor.GetBaseFolder() + Path.DirectorySeparatorChar + "Scripts";

        public string GetPersonalityFolder(string dommePersonalityName) =>
            GetPersonalitiesFolder() + Path.DirectorySeparatorChar + dommePersonalityName;

        public string GetScriptCld(string dommePersonalityName, SessionPhase sessionPhase)
        {
            if (sessionPhase == SessionPhase.Modules)
                return GetPersonalityFolder(dommePersonalityName) + Path.DirectorySeparatorChar + "System" + Path.DirectorySeparatorChar + "ModuleCheckList.cld";
            else
                return GetPersonalityFolder(dommePersonalityName) + Path.DirectorySeparatorChar + "System" + Path.DirectorySeparatorChar + sessionPhase.ToString() + "CheckList.cld";
        }

        public string GetScriptDir(string dommePersonalityName, string type, SessionPhase sessionPhase)
        {
            var baseDir = GetPersonalityFolder(dommePersonalityName) + Path.DirectorySeparatorChar;

            if (sessionPhase == SessionPhase.Modules)
                baseDir += "Modules" + Path.DirectorySeparatorChar;
            else
                baseDir += type + Path.DirectorySeparatorChar + sessionPhase.ToString() + Path.DirectorySeparatorChar;

            return baseDir;
        }
    }
}