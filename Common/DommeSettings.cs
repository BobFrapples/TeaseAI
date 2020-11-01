using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Settings specific to the Domme
    /// </summary>
    public class DommeSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DommeSettings()
        {
            Version = 1;
            DominationLevel = DomLevel.Tease;
            ApathyLevel = ApathyLevel.Moderate;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Full file to the Domme's avatar image 
        /// </summary>
        public string AvatarImageFile { get; set; }

        /// <summary>
        /// The name of the Domme
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// What is the Domme's Birth date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// how old is the dommme. current date - birthdate
        /// </summary>
        public int Age => DateTime.Now.Year - BirthDate.Year;

        /// <summary>
        /// used to limit orgasms. 
        /// </summary>
        [JsonIgnore]
        public bool AreOrgasmsLocked => OrgasmReleaseDate <= DateTime.Now.Date;

        /// <summary>
        /// How caring does the Domme start each session
        /// </summary>
        public ApathyLevel ApathyLevel { get; set; }

        /// <summary>
        /// How heavily will the Domme tease
        /// </summary>
        public DomLevel DominationLevel { get; set; }

        /// <summary>
        /// When can the sub have unlimited orgasms again. 
        /// </summary>
        public DateTime OrgasmReleaseDate { get; set; }

        /// <summary>
        /// Does the Domme have Tattoos
        /// </summary>
        public bool HasTattoos { get; set; }

        /// <summary>
        /// Does the Domme have Freckles
        /// </summary>
        public bool HasFreckles { get; set; }

        /// <summary>
        /// What color is the domme's hair
        /// </summary>
        public string HairColor { get; set; }

        /// <summary>
        /// How long is the dommes hair
        /// </summary>
        public string HairLength { get; set; }

        public string EyeColor { get; set; }

        /// <summary>
        /// How big are the domme's boobs
        /// </summary>
        public CupSize CupSize { get; set; }

        /// <summary>
        /// What style is the Domme's pubic hair
        /// </summary>
        public string PubicHair { get; set; }

        /// <summary>
        /// Is the domme crazy
        /// </summary>
        public bool IsCrazy { get; set; }

        /// <summary>
        /// Is the domme Vulgar
        /// </summary>
        public bool IsVulgar { get; set; }

        /// <summary>
        /// Is the Domme a Female Supremicist
        /// </summary>
        public bool IsSupremacist { get; set; }

        /// <summary>
        /// Should the domme type in all lowercase
        /// </summary>
        public bool UseLowercase { get; set; }

        /// <summary>
        /// Should the domme skip apostrophes
        /// </summary>
        public bool UseNoApostrophes { get; set; }
        public bool UseNoCommas { get; set; }
        public bool UseNoPeriods { get; set; }
        public bool CapitalizeSelfPronouns { get; set; }

        public List<string> PetNames
        {
            get
            {
                return _petNames ?? (_petNames = new List<string>(8));
            }
            set
            {
                _petNames = value;
            }
        }

        /// <summary>
        /// how likely is the domme to allow an orgasm (combines with RuinsOrgams)
        /// </summary>
        public AllowsOrgasms AllowsOrgasms { get; set; }

        /// <summary>
        /// how likely is this domme to ruin the subs orgasm
        /// </summary>
        public RuinsOrgasms RuinsOrgasms { get; set; }
        public bool IsOrgasmChanceLocked { get; set; }
        public bool DoesDenialEndTease { get; set; }
        public bool DoesOrgasmEndTease { get; set; }

        /// <summary>
        /// how many orgasms the domme lets the sub orgasm. i.e. orgasms / range
        /// </summary>
        public int OrgasmsTimePeriodDays { get; set; }

        /// <summary>
        /// the time period the domme will let the sub orgasm. i.e. orgasms / range
        /// </summary>
        public int OrgasmsPerTimePeriod { get; set; }

        /// <summary>
        /// The domme is in a bad mood up (but not including) to this point
        /// </summary>
        public int BadMoodThreshold { get; set; }

        /// <summary>
        /// The domme is in a good mood above (but not including) to this point
        /// </summary>
        public int GoodMoodThreshold { get; set; }

        /// <summary>
        /// Under this value (but not including) the Domme considers a penis small
        /// </summary>
        public int AveragePenisMinimum { get; set; }

        /// <summary>
        /// over this value (but not including) the Domme considers a penis Large
        /// </summary>
        public int AveragePenisMaximum { get; set; }

        /// <summary>
        /// Under this number, the Domme thinks of herself as young
        /// </summary>
        public int AverageAgeSelfMinimum { get; set; }

        /// <summary>
        /// Over this number, the Domme thinks of herself as old
        /// </summary>
        public int AverageAgeSelfMaximum { get; set; }

        /// <summary>
        /// Under this number, the Domme things of the sub as young
        /// </summary>
        public int AverageAgeSubMinimum { get; set; }

        /// <summary>
        /// Over this number, the Domme thinks of the sub as old
        /// </summary>
        public int AverageAgeSubMaximum { get; set; }

        /// <summary>
        /// Title used to address the Domme, defaults to Mistress
        /// </summary>
        public string Honorific
        {
            get => string.IsNullOrWhiteSpace(_honorific) ? "Mistress" : _honorific;
            set => _honorific = value;
        }

        /// <summary>
        /// Does the Domme require the use of a title. 
        /// </summary>
        public bool RequiresHonorific { get; set; }

        /// <summary>
        /// Does the Domme require the Honorific be capitalized (Mistress vs mistress)
        /// </summary>
        public bool RequiresHonorificCapitalized { get; set; }

        private string _honorific;
        private List<string> _petNames;
    }
}
