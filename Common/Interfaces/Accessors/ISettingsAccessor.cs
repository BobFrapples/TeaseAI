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
        string SubName { get; set; }

        #region Domme settings
        string DommeAvatarImageName { get; set; }
        string DommeName { get; set; }
        #endregion

        Dictionary<ImageGenre, bool> ImageGenreIncludeSubDirectory { get; }
        Dictionary<ImageGenre, bool> IsImageGenreEnabled { get; }
        Dictionary<ImageGenre, string> ImageGenreFolder { get; }

  
        bool IsOffline { get; set; }

        bool DoesDommeTypeInstantly { get; set; }
        bool WebTeaseModeEnabled { get; set; }
        bool InChastity { get; set; }
        int BronzeTokens { get; set; }
        int SilverTokens { get; set; }
        int GoldTokens { get; set; }

        void Save();
    }
}
