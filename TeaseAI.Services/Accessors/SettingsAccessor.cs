using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.Serialization;

namespace TeaseAI.Services.Accessors
{
    public class SettingsAccessor : ISettingsAccessor
    {
        public SettingsAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Settings GetSettings()
        {

            //var settingsFile = _configurationAccessor.GetSettingsLocation();
            //if (File.Exists(settingsFile))
            //{
            //    var jsonString = File.ReadAllText(settingsFile);
            //    return Deserialize(jsonString);
            //}
            var settings = CreateDefaultSettings();
            return WriteSettings(settings);
        }

        public Settings WriteSettings(Settings settings)
        {
            var settingsFile = _configurationAccessor.GetSettingsLocation();
            File.WriteAllText(settingsFile, Serialize(settings));
            return settings;
        }

        /// <summary>
        /// Create a default settings object
        /// </summary>
        /// <returns></returns>
        public Settings CreateDefaultSettings()
        {
            return new Settings
            {
                DommePersonality = "dev-wicked-tease",
                Domme = new DommeSettings
                {
                    AvatarImageFile = string.Empty,
                    Name = "Domme Name",
                    BirthDate = new DateTime(DateTime.Now.Year - 21, 1, 1),
                    AllowsOrgasms = AllowsOrgasms.Sometimes,
                    RuinsOrgasms = RuinsOrgasms.Sometimes,
                    OrgasmReleaseDate = DateTime.MinValue,
                    OrgasmsPerTimePeriod = 3,
                    OrgasmsTimePeriodDays = 180,
                    CapitalizeSelfPronouns = false,
                    ApathyLevel = ApathyLevel.Moderate,
                    DominationLevel = DomLevel.Tease,
                    DoesDenialEndTease = true,
                    DoesOrgasmEndTease = true,
                    CupSize = CupSize.CCup,
                    EyeColor = "green",
                    HairColor = "blonde",
                    HairLength = "long",
                    HasFreckles = false,
                    HasTattoos = false,
                    IsVulgar = false,
                    IsSupremacist = false,
                    IsOrgasmChanceLocked = false,
                    PubicHair = "shaved",
                    UseLowercase = false,
                    UseNoApostrophes = false,
                    UseNoCommas = false,
                    UseNoPeriods = false,
                    PetNames = new List<string>
                    {
                        "stroker",
                        "stroker",
                        "stroker",
                        "stroker",
                        "stroker",
                        "stroker",
                        "stroker",
                        "stroker",
                    },
                    BadMoodThreshold = 3,
                    GoodMoodThreshold = 8,
                    AveragePenisMinimum = 6,
                    AveragePenisMaximum = 8,
                    AverageAgeSelfMinimum = 28,
                    AverageAgeSelfMaximum = 49,
                    AverageAgeSubMinimum = 28,
                    AverageAgeSubMaximum = 49,
                },
                General = new GeneralSettings
                {
                    CanDommeDeleteFiles = false,
                    IsTimeStampEnabled = true,
                    ShowChatUserNames = true,
                    DoesDommeTypeInstantly = false,
                    IsWebTeaseModeEnabled = false,
                },
                Sub = new SubSettings
                {
                    AvatarImageFile = string.Empty,
                    Name = "sub name",
                    Safeword = "red",
                    CanInterruptLongEdge = true,
                    AllowLongEdgeInterrupts = true,
                    AllowLongEdgeTaunts = false,
                    CallBallsPussy = false,
                    CallCockAClit = false,
                    HasChastityDevice = false,
                    DoesChastityDeviceContainSpikes = false,
                    DoesChastityDeviceRequirePiercing = false,
                    ExtremeEdgeHoldMinimum = 120,
                    ExtremeEdgeHoldMaximum = 180,
                    CockAndBallTortureLevel = TortureLevel.Create(3).Value,
                    IsBallTortureEnabled = false,
                    IsCockTortureEnabled = false,
                    IsSubCircumcised = false,
                    HoldEdgeSecondsMaximum = 60,
                    HoldEdgeSecondsMinimum = 10,
                    IsSubFemale = false,
                    IsSubPierced = false,
                    LongEdgeHoldMaximum = 120,
                    LongEdgeHoldMinimum = 60,
                    LongEdgeThreshold = 45,
                    UseAverageEdgeTimeAsThreshold = false,
                },
                Range = new RangeSettings
                {
                    DoesDommeDecideOrgasmRange = true,
                    DoesDommeDecideRuinRange = true,
                    AllowOrgasmRarelyPercent = AllowsOrgasms.Rarely,
                    AllowOrgasmSometimesPercent = AllowsOrgasms.Sometimes,
                    AllowOrgasmOftenPercent = AllowsOrgasms.Often,
                    RuinOrgasmRarelyPercent = RuinsOrgasms.Rarely,
                    RuinOrgasmSometimesPercent = RuinsOrgasms.Sometimes,
                    RuinOrgasmOftenPercent = RuinsOrgasms.Often,
                    IsTauntCycleDommeDetermined = false,
                    IsTeaseLengthDommeDetermined = false,
                    TeaseLengthMinutesMaximum = 60,
                    TeaseLengthMinutesMinimum = 15,
                    TauntCycleMinutesMaximum = 5,
                    TauntCycleMinutesMinimum = 1,
                },
                Misc = new MiscSettings
                {
                    IsOffline = false,
                    IsInChastity = false,
                }
            };
        }

        public Settings Deserialize(string settingsJson)
        {
            var jsonConverters = CreateCustomJsonConverters();
            return JsonConvert.DeserializeObject<Settings>(settingsJson, jsonConverters.ToArray());
        }

        public string Serialize(Settings settings)
        {
            var jsonConverters = CreateCustomJsonConverters();
            return JsonConvert.SerializeObject(settings, Formatting.Indented, jsonConverters.ToArray());
        }

        private List<JsonConverter> CreateCustomJsonConverters()
        {
            var jsonConverters = new List<JsonConverter>();
            jsonConverters.Add(new ApathyLevelJsonConverter());
            jsonConverters.Add(new DomLevelJsonConverter());
            jsonConverters.Add(new TortureLevelJsonConverter());
            return jsonConverters;
        }

        public List<string> GetGreetings()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private readonly IConfigurationAccessor _configurationAccessor;

        public string SubName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DommeAvatarImageName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DommeName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dictionary<ImageGenre, bool> ImageGenreIncludeSubDirectory => throw new NotImplementedException();

        public Dictionary<ImageGenre, bool> IsImageGenreEnabled => throw new NotImplementedException();

        public Dictionary<ImageGenre, string> ImageGenreFolder => throw new NotImplementedException();

        public bool WebTeaseModeEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BronzeTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SilverTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int GoldTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}