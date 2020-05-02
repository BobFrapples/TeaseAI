using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// Application level configurations Install path, etc.
    /// Anything to be read from app.config. This isn't for user configs
    /// </summary>
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// Get the applicationConfiguration
        /// </summary>
        /// <returns></returns>
        ApplicationConfiguration GetApplicationConfiguration();

        /// <summary>
        /// Save <paramref name="applicationConfiguration"/>
        /// </summary>
        /// <param name="applicationConfiguration"></param>
        /// <returns></returns>
        Result SaveApplicationConfiguration(ApplicationConfiguration applicationConfiguration);

        /// <summary>
        /// The root folder for image / videos / all other data
        /// </summary>
        string GetBaseFolder();

        /// <summary>
        /// Get the full path and filename to the SQLite database file. Will create it if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        string GetDatabaseConnectionString();

    }
}
