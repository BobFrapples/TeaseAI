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

        //Result<List<ScriptMetaData>> GetAvailableScripts(Session session, string type, string stage);
        Result<List<ScriptMetaData>> GetAvailableScripts(DommePersonality domme, SubPersonality submissive, string type, SessionPhase stage);

        /// <summary>
        /// Get all scripts for <paramref name="domme"/>
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="type"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        List<ScriptMetaData> GetAllScripts(string dommePersonalityName, string type, SessionPhase stage, bool isEnabledDefault);

        Result<Script> GetScript(ScriptMetaData value);

        /// <summary>
        /// Get a script using the path relative to <paramref name="domme"/> personality.
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Result<Script> GetScript(DommePersonality domme, string fileName);
        /// <summary>
        /// Save all script information for <paramref name="dommePersonalityName"/>
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="dommePersonalityName"></param>
        /// <param name="type"></param>
        /// <param name="stage"></param>
        void Save(List<ScriptMetaData> scripts, string dommePersonalityName, string type, SessionPhase stage);
    }
}
