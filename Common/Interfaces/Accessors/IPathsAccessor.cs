using TeaseAI.Common.Constants;

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

        /// <summary>
        /// Get the system images folder
        /// </summary>
        /// <returns></returns>
        string GetSystemImages();

        /// <summary>
        /// Get the path where vitalsub data is located
        /// </summary>
        /// <returns></returns>
        string GetVitalSubDir();

        /// <summary>
        /// Get path for Domme specific vital sub data
        /// </summary>
        /// <param name="dommePersonalityName"></param>
        /// <returns></returns>
        string GetVitalSubDir(string dommePersonalityName);
    }
}
