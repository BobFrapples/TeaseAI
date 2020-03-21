using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services
{
    public class PersonalityService : IPersonalityService
    {
        private readonly IPathsAccessor _pathsAccessor;

        public PersonalityService(IPathsAccessor pathsAccessor)
        {
            _pathsAccessor = pathsAccessor;
        }

        public List<Personality> GetAllPersonalities()
        {
            var personalityFolder = _pathsAccessor.GetPersonalitiesFolder();
            Directory.CreateDirectory(personalityFolder);
            var personalities = Directory.GetDirectories(personalityFolder).ToList();

            return personalities
                .Select(p => new Personality() { Id = p, Name = Path.GetFileName(p) })
                .ToList();
        }
    }
}
