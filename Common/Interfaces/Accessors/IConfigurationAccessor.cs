namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// Application level configurations Install path, etc.
    /// Anything to be read from app.config. This isn't for user configs
    /// </summary>
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// The root folder for image / videos / all other data
        /// </summary>
         string GetBaseFolder();
    }
}
