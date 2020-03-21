using System.Collections.Generic;

namespace TeaseAI.Common.Interfaces
{
    public interface IGetCommandProcessorsService
    {
        /// <summary>
        /// Create all command processors currently implemented
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ICommandProcessor> CreateCommandProcessors();
    }
}
