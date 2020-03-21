using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// Get informatoin about commands
    /// </summary>
    public interface IGetCommandInformationAccessor
    {
        Result<ScriptCommandInformation> GetCommandInformation(string command);
    }
}
