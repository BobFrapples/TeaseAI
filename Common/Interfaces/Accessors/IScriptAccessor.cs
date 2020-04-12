using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IScriptAccessor
    {
        /// <summary>
        /// This will return the fall back script if no others are available
        /// </summary>
        /// <param name="session"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        Result<ScriptMetaData> GetFallbackMetaData(Session session, SessionPhase stage);

        Result<List<ScriptMetaData>> GetAvailableScripts(DommePersonality domme, SubPersonality submissive, string type, SessionPhase stage);

        /// <summary>
        /// Get all scripts for <paramref name="domme"/>
        /// </summary>
        /// <param name="dommePersonalityName"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        List<ScriptMetaData> GetAllScripts(string dommePersonalityName,  SessionPhase stage);

        /// <summary>
        /// Get all scripts for a domme
        /// </summary>
        /// <param name="dommePersonalityName"></param>
        /// <returns></returns>
        Result<List<ScriptMetaData>> GetAllScripts(string dommePersonalityName);

        Result<Script> GetScript(ScriptMetaData value);

        /// <summary>
        /// Save a script, but not the metadata
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        Result Save(Script script);

        /// <summary>
        /// Get a script using the path relative to <paramref name="domme"/> personality.
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Result<Script> GetScript(DommePersonality domme, string fileName);

        /// <summary>
        /// Get the script identified by <paramref name="Id"/>
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Result<Script> GetScript(string Id);

        /// <summary>
        /// Save all script metadata for <paramref name="dommePersonalityName"/>
        /// This *MUST* include all metadata.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="dommePersonalityName"></param>
        /// <param name="type"></param>
        /// <param name="stage"></param>
        void Save(List<ScriptMetaData> scripts, string dommePersonalityName, string type, SessionPhase stage);
    }
}
