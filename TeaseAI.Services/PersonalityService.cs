using System;
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
        private readonly string _baseFolder;

        public PersonalityService(IConfigurationAccessor configurationAccessor)
        {
            _baseFolder = configurationAccessor.GetBaseFolder();
        }

        public List<Personality> GetAllPersonalities()
        {
            var personalityFolder = _baseFolder + Path.DirectorySeparatorChar + "personalities";
            Directory.CreateDirectory(personalityFolder);
            var personalities = Directory.GetDirectories(personalityFolder).ToList();

            return personalities
                .Select(p => new Personality() { Id = p, Name = Path.GetFileName(p) })
                .ToList();
        }
    }
}
