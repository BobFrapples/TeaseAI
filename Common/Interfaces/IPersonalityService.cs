using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IPersonalityService
    {
        /// <summary>
        /// Get a collection of all known personalties
        /// </summary>
        /// <returns></returns>
        List<Personality> GetAllPersonalities();
    }
}
