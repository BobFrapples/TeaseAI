using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IPersonalityService
    {
        List<Personality> GetAllPersonalities();
    }
}
