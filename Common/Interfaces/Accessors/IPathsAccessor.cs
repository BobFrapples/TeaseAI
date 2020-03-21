﻿using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IPathsAccessor
    {
        string RiskyPickScript { get; }

        /// <summary>
        /// Get the folder which has all personalities in it
        /// </summary>
        /// <returns></returns>
        string GetPersonalitiesFolder();

        /// <summary>
        /// Get the folder for <paramref name="dommePersonalityName"/>
        /// </summary>
        /// <param name="dommePersonalityName"></param>
        /// <returns></returns>
        string GetPersonalityFolder(string dommePersonalityName);

        /// <summary>
        /// Get the directory storing the scripts
        /// </summary>
        /// <param name="dommePersonalityName"></param>
        /// <param name="type"></param>
        /// <param name="sessionPhase"></param>
        /// <returns></returns>
        string GetScriptDir(string dommePersonalityName, string type, SessionPhase sessionPhase);
        string GetScriptCld(string dommePersonalityName, SessionPhase sessionPhase);
    }
}