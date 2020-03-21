using System;
using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class SettingsAccessor : ISettingsAccessor
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public SettingsAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public string SubName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsCockTortureEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsBallTortureEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesChastityDeviceRequirePiercing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesChastityDeviceContainSpikes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CanInterruptLongEdge { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int HoldEdgeMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int HoldEdgeMinimum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int LongHoldEdgeMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int LongHoldEdgeMinimum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ExtremeHoldEdgeMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ExtremeHoldEdgeMinimum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TortureLevel CockAndBallTortureLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSubCircumcised { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSubPierced { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DommePersonality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DommeAvatarImageName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DommeName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DomLevel DominationLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ApathyLevel ApathyLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesDommeDecideOrgasmRange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesDommeDecideRuinRange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AllowOrgasmOftenPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AllowOrgasmSometimesPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AllowOrgasmRarelyPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RuinOrgasmOftenPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RuinOrgasmSometimesPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RuinOrgasmRarelyPercent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SafeWord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseAverageEdgeTimeAsThreshold { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AllowsLongEdgeTaunts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AllowsLongEdgeInterrupts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsTeaseLengthDommeDetermined { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsTauntCycleDommeDetermined { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool HasChastityDevice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSubFemale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CanDommeDeleteFiles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TeaseLengthMinimum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TeaseLengthMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TauntCycleMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TauntCycleMinimum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CallCockAClit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CallBallsPussy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dictionary<ImageGenre, bool> ImageGenreIncludeSubDirectory => throw new NotImplementedException();

        public Dictionary<ImageGenre, bool> IsImageGenreEnabled => throw new NotImplementedException();

        public Dictionary<ImageGenre, string> ImageGenreFolder => throw new NotImplementedException();

        public bool AreOrgasmsLocked => throw new NotImplementedException();

        public DateTime OrgasmLockDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsOffline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsTimeStampEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShowNames { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesDommeTypeInstantly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool WebTeaseModeEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool InChastity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BronzeTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SilverTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int GoldTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> GetGreetings()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}