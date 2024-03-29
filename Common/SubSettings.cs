﻿using System;
using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Settings specific to the sub
    /// </summary>
    public class SubSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// What word does the sub use to indicate a problem. UI puts this in domme section, but it is typically defined by the sub.
        /// </summary>
        public string Safeword { get; set; }

        /// <summary>
        /// This appears unused.
        /// </summary>
        public bool CanInterruptLongEdge { get; set; }

        /// <summary>
        /// Maximum seconds to hold an edge
        /// </summary>
        public int HoldEdgeSecondsMaximum { get; set; }

        /// <summary>
        /// Minimum seconds to hold an edge
        /// </summary>
        public int HoldEdgeSecondsMinimum { get; set; }

        /// <summary>
        /// Should interrupts be allowed for a long edge
        /// </summary>
        public bool AllowLongEdgeInterrupts { get; set; }

        /// <summary>
        /// Should taunts be allowed for a long edge
        /// </summary>
        public bool AllowLongEdgeTaunts { get; set; }

        /// <summary>
        /// Should balls be referred to as a pussy
        /// </summary>
        public bool CallBallsPussy { get; set; }

        /// <summary>
        /// Should penis be referred to as a clit
        /// </summary>
        public bool CallCockAClit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TortureLevel CockAndBallTortureLevel { get; set; }

        /// <summary>
        /// Does the sub own a chastity device
        /// </summary>
        public bool HasChastityDevice { get; set; }

        /// <summary>
        /// Does the chastity device contain spikes. should only be true if <see cref="HasChastityDevice"/> is also true
        /// </summary>
        public bool DoesChastityDeviceContainSpikes { get; set; }

        /// <summary>
        /// Does the chastity device require a  piercing. should only be true if <see cref="HasChastityDevice"/> is also true
        /// </summary>
        public bool DoesChastityDeviceRequirePiercing { get; set; }

        /// <summary>
        /// Does the sub want cock (clit) torture
        /// </summary>
        public bool IsCockTortureEnabled { get; set; }

        /// <summary>
        /// Does the sub want ball (pussy) torture
        /// </summary>
        public bool IsBallTortureEnabled { get; set; }

        /// <summary>
        /// Not exactly sure what this is for
        /// </summary>
        public int LongEdgeThreshold { get; set; }

        /// <summary>
        /// what is the maximum time (in seconds) to hold a long edge
        /// </summary>
        public int LongEdgeHoldMaximum { get; set; }

        /// <summary>
        /// What is the minimum time (in seconds) to hold a long edge
        /// </summary>
        public int LongEdgeHoldMinimum { get; set; }

        /// <summary>
        /// What is the maximum time (in seconds) to hold an extreme edge
        /// </summary>
        public int ExtremeEdgeHoldMaximum { get; set; }

        /// <summary>
        /// What is the minimum time (in seconds) to hold an extreme edge
        /// </summary>
        public int ExtremeEdgeHoldMinimum { get; set; }

        /// <summary>
        /// Is the sub circumcised
        /// </summary>
        public bool IsSubCircumcised { get; set; }

        /// <summary>
        /// Is the sub pierced
        /// </summary>
        public bool IsSubPierced { get; set; }

        /// <summary>
        /// Unsure exactly what this is.
        /// </summary>
        public bool UseAverageEdgeTimeAsThreshold { get; set; }

        /// <summary>
        ///  Does the sub use female pronounce
        /// </summary>
        public bool IsSubFemale { get; set; }

        /// <summary>
        /// Image file for the sub
        /// </summary>
        public string AvatarImageFile { get; set; }

        /// <summary>
        /// Name of the sub
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// list of greetings the sub may use
        /// </summary>
        public List<string> Greetings
        {
            get { return _greetings ?? (_greetings = new List<string>()); }
            set { _greetings = value; }
        }

        /// <summary>
        /// how long is the cock attached to the sub
        /// </summary>
        public int CockLength { get; set; }

        /// <summary>
        /// When was the sub born
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// how old is the sub. current date - birthdate
        /// </summary>
        public int Age => DateTime.Now.Year - BirthDate.Year;

        /// <summary>
        /// List of phrases the sub can use to indicate yes
        /// </summary>
        public List<string> YesPhrases
        {
            get { return _yesPhrases ?? (_yesPhrases = new List<string>()); }
            set { _yesPhrases = value; }
        }

        /// <summary>
        /// List of phrases the sub can use to indicate no
        /// </summary>
        public List<string> NoPhrases
        {
            get { return _noPhrases ?? (_noPhrases = new List<string>()); }
            set { _noPhrases = value; }
        }

        /// <summary>
        /// hair color of the sub
        /// </summary>
        public string HairColor { get; set; }

        /// <summary>
        /// eye color of the sub
        /// </summary>
        public string EyeColor { get; set; }

        /// <summary>
        /// Configuration for the "VitalSub" feature
        /// </summary>
        public HealthGoals HealthGoals
        {
            get => _healthGoals ?? (new HealthGoals());
            set => _healthGoals = value;
        }

        private List<string> _greetings;
        private List<string> _yesPhrases;
        private List<string> _noPhrases;
        private HealthGoals _healthGoals;
    }
}
