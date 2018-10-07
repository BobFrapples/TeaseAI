using System;
using System.Collections.Generic;
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
        Result<ScriptMetaData> GetFallbackMetaData(Session session, string stage);

        //Result<List<ScriptMetaData>> GetAvailableScripts(Session session, string type, string stage);
        Result<List<ScriptMetaData>> GetAvailableScripts(DommePersonality domme, SubPersonality submissive, string type, string stage);

        Result<Script> GetScript(ScriptMetaData value);
        /// <summary>
        /// Get a script using the path relative to <paramref name="domme"/> personality.
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Result<Script> GetScript(DommePersonality domme, string fileName);
    }
}
