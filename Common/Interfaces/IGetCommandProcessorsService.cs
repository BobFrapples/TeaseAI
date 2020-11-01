using System.Collections.Generic;

namespace TeaseAI.Common.Interfaces
{
    /// <summary>
    /// Get a dictionary of all command processors, indexed by the keyword they process
    /// </summary>
    public interface IGetCommandProcessorsService
    {
        /// <summary>
        /// Create all command processors currently implemented
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ICommandProcessor> CreateCommandProcessors();
    }
}
