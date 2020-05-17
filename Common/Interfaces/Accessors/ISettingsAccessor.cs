using System;
using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// User configurable settings.
    /// </summary>
    public interface ISettingsAccessor
    {
        /// <summary>
        /// Get the current settings for the application
        /// </summary>
        /// <returns></returns>
        Settings GetSettings();

        /// <summary>
        /// Write <paramref name="settings"/> to long term storage
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Settings WriteSettings(Settings settings);

        List<string> GetGreetings();
   
        Dictionary<ImageGenre, string> ImageGenreFolder { get; }
        int BronzeTokens { get; set; }
        int SilverTokens { get; set; }
        int GoldTokens { get; set; }

        void Save();
    }
}
