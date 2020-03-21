using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// Get informatoin about commands
    /// </summary>
    public interface IGetCommandInformationAccessor
    {
        /// <summary>
        /// Get a list of all commands we have documentation for
        /// </summary>
        /// <returns></returns>
        List<string> GetAvailableCommands();

        /// <summary>
        /// Get the documenatation information for <paramref name="command"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Result<ScriptCommandInformation> GetCommandInformation(string command);
    }
}
