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
    }
}