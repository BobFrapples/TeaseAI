using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    public class DommePersonality
    {
        /// <summary>
        /// Age of the dom
        /// </summary>
        public ushort Age { get; set; }

        /// <summary>
        /// Age under which the dom considers themself young
        /// </summary>
        public ushort AgeYoungLimit { get; set; }

        /// <summary>
        /// Age over which the dom considers themself old
        /// </summary>
        public ushort AgeOldLimit { get; set; }

        public AllowsOrgasms AllowsOrgasms { get; set; }
        public ApathyLevel ApathyLevel { get; set; }

        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Length over which the dom considers penises to be big
        /// </summary>
        public ushort CockBigLimit { get; set; }

        /// <summary>
        /// Length under which the dom considers penises to be small
        /// </summary>
        public ushort CockSmallLimit { get; set; }

        public CupSize CupSize { get; set; }
        /// <summary>
        /// Domination level, how mean the domme is.
        /// </summary>
        public DomLevel DomLevel { get; set; }

        /// <summary>
        /// What is the Domme's title, Princess, Mistress, etc
        /// </summary>
        public string Honorific { get; set; }

        public bool IsCrazy { get; set; }
        public bool IsDegrading { get; set; }
        public bool IsSadistic { get; set; }
        public bool IsSupremacist { get; set; }
        public bool IsVulgar { get; set; }

        /// <summary>
        /// The Domme's current mood
        /// </summary>
        public MoodLevel MoodLevel { get; set; }

        /// <summary>
        /// The level at which the Domme gets angry
        /// </summary>
        public MoodLevel MoodAngry { get; set; }
        /// <summary>
        /// The level at which the Domme becomes happy
        /// </summary>
        public MoodLevel MoodHappy { get; set; }

        /// <summary>
        /// the name of the domme
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is the name the program uses to find files on the Domme
        /// </summary>
        public string PersonalityName { get; set; }

        public RuinsOrgasms RuinsOrgasms { get; set; }

        /// <summary>
        /// Age under which the dom considers the sub young
        /// </summary>
        public ushort SubAgeYoungLimit { get; set; }

        /// <summary>
        /// Age over which the dom considers the sub old
        /// </summary>
        public ushort SubAgeOldLimit { get; set; }
        public bool RequiresHonorific { get; set; }
        public bool RequiresHonorificCapitalized { get; set; }

        /// <summary>
        /// Was the domme greeted properly
        /// </summary>
        public bool WasGreeted { get; set; }

        /// <summary>
        /// Time in milliseconds the domme will wait between reading lines in the script.
        /// </summary>
        public int MessageTimer { get; set; } = 2000;

        public DommePersonality Clone()
        {
            return new DommePersonality()
            {
                Age = Age,
                AgeOldLimit = AgeOldLimit,
                AgeYoungLimit = AgeYoungLimit,
                AllowsOrgasms = AllowsOrgasms,
                ApathyLevel = ApathyLevel,
                BirthDay = BirthDay,
                CockBigLimit = CockBigLimit,
                CockSmallLimit = CockSmallLimit,
                CupSize = CupSize,
                DomLevel = DomLevel,
                Honorific = Honorific,
                IsCrazy = IsCrazy,
                IsDegrading = IsDegrading,
                IsSadistic = IsSadistic,
                IsSupremacist = IsSupremacist,
                IsVulgar = IsVulgar,
                MoodLevel = MoodLevel,
                MoodAngry = MoodAngry,
                MoodHappy = MoodHappy,
                Name = Name,
                PersonalityName = PersonalityName,
                RuinsOrgasms = RuinsOrgasms,
                SubAgeOldLimit = SubAgeOldLimit,
                SubAgeYoungLimit = SubAgeYoungLimit,
                RequiresHonorific = RequiresHonorific,
                RequiresHonorificCapitalized = RequiresHonorificCapitalized,
                WasGreeted = WasGreeted,
            };
        }

    }
}
