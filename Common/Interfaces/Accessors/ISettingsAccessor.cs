using System;
using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface ISettingsAccessor
    {
        List<string> GetGreetings();
        string GetSubName();

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
        string DommePersonality { get; set; }
        string DommeAvatarImageName { get; set; }
        string DommeName { get; set; }
        DomLevel DominationLevel { get; set; }
        ApathyLevel ApathLevel { get; set; }
        bool DoesDommeDecideOrgasmRange { get; set; }
        bool DoesDommeDecideRuinRange { get; set; }
        int AllowOrgasmOftenPercent { get; set; }
        int AllowOrgasmSometimesPercent { get; set; }
        int AllowOrgasmRarelyPercent { get; set; }

        int RuinOrgasmOftenPercent { get; set; }
        int RuinOrgasmSometimesPercent { get; set; }
        int RuinOrgasmRarelyPercent { get; set; }
        string SafeWord { get; set; }
        #endregion

        bool UseAverageEdgeTimeAsThreshold { get; set; }
        bool AllowsLongEdgeTaunts { get; set; }
        bool AllowsLongEdgeInterrupts { get; set; }
        bool IsTeaseLengthDommeDetermined { get; set; }
        bool IsTauntCycleDommeDetermined { get; set; }
        bool HasChastityDevice { get; set; }
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
        /// Should the scripts be audited on startup
        /// </summary>
        bool ShouldAuditScripts { get; set; }
    }
}
