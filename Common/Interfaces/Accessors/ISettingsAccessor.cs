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

        #region Sub settings
        bool IsCockTortureEnabled { get; set; }
        bool IsBallTortureEnabled { get; set; }
        bool DoesChastityDeviceRequirePiercing { get; set; }
        bool DoesChastityDeviceContainSpikes { get; set; }
        bool CanInterruptLongEdge { get; set; }
        int HoldEdgeMaximum { get; set; }
        int HoldEdgeMinimum { get; set; }
        int LongHoldEdgeMaximum { get; set; }
        int LongHoldEdgeMinimum { get; set; }
        int ExtremeHoldEdgeMaximum { get; set; }
        int ExtremeHoldEdgeMinimum { get; set; }
        TortureLevel CockAndBallTortureLevel { get; set; }
        bool IsSubCircumcised { get; set; }
        bool IsSubPierced { get; set; }
        #endregion

        #region Domme settings
        string DommeAvatarImageName { get; set; }
        string DommeName { get; set; }
        //bool DoesDommeDecideOrgasmRange { get; set; }
        //bool DoesDommeDecideRuinRange { get; set; }
        //int AllowOrgasmOftenPercent { get; set; }
        //int AllowOrgasmSometimesPercent { get; set; }
        //int AllowOrgasmRarelyPercent { get; set; }

        //int RuinOrgasmOftenPercent { get; set; }
        //int RuinOrgasmSometimesPercent { get; set; }
        //int RuinOrgasmRarelyPercent { get; set; }
        #endregion

        bool UseAverageEdgeTimeAsThreshold { get; set; }
        bool AllowsLongEdgeTaunts { get; set; }
        bool AllowsLongEdgeInterrupts { get; set; }
        bool IsTeaseLengthDommeDetermined { get; set; }
        bool IsTauntCycleDommeDetermined { get; set; }
        bool HasChastityDevice { get; set; }
        /// <summary>
        /// Does the sub identify as female
        /// </summary>
        bool IsSubFemale { get; set; }
        bool CanDommeDeleteFiles { get; set; }
        int TeaseLengthMinimum { get; set; }
        int TeaseLengthMaximum { get; set; }
        int TauntCycleMaximum { get; set; }
        int TauntCycleMinimum { get; set; }
        bool CallCockAClit { get; set; }
        bool CallBallsPussy { get; set; }
        Dictionary<ImageGenre, bool> ImageGenreIncludeSubDirectory { get; }
        Dictionary<ImageGenre, bool> IsImageGenreEnabled { get; }
        Dictionary<ImageGenre, string> ImageGenreFolder { get; }

        /// <summary>
        /// Are orgasms currently locked. true if <see cref="OrgasmLockDate"/> is in the future
        /// </summary>
        bool AreOrgasmsLocked { get; }

        /// <summary>
        /// Date orgasms are locked until
        /// </summary>
        DateTime OrgasmLockDate { get; set; }
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
