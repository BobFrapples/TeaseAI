using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    public class SubPersonality
    {
        public ushort Age { get; set; }
        public TortureLevel BallsTortureLevel { get; set; }
        public DateTime Birthday { get; set; }
        public int CockSize { get; set; }
        public TortureLevel CockTortureLevel { get; set; }

        public bool InChastity { get; set; }
        public bool IsCircumsized { get; set; }
        public bool IsCockPierced { get; set; }

        /// <summary>
        /// Is the sub stroking
        /// </summary>
        public bool IsStroking { get; set; }

        /// <summary>
        /// Is the sub edging
        /// </summary>
        public bool IsEdging { get; set; }

        /// <summary>
        /// Should the sub hold the edge, evaluates to HoldEdgeSeconds > 0
        /// </summary>
        public bool IsHoldingTheEdge => HoldEdgeSeconds > 0;

        /// <summary>
        /// Sub's name
        /// </summary>
        public string Name { get => _name ?? (_name = string.Empty); set => _name = value; }

        /// <summary>
        /// List of things the sub likes. Will never be null(Nothing in VB)
        /// </summary>
        public List<string> Kinks
        {
            get { return _kinks ?? (_kinks = new List<string>()); }
            set { _kinks = value; }
        }

        /// <summary>
        /// Lisst of toys the sub owns. Will never be null(Nothing in VB)
        /// </summary>
        public List<string> ToyBox
        {
            get { return _toyBox ?? (_toyBox = new List<string>()); }
            set { _toyBox = value; }
        }

        public string Safeword { get => _safeword ?? (_safeword = string.Empty); set => _safeword = value; }
        private string _safeword;
        public bool? WillBeAllowedToOrgasm { get; set; }
        public bool IsOrgasmRestricted { get; set; }
        public int StrokePace { get; set; }

        /// <summary>
        /// All pet names that might be used
        /// </summary>
        public List<string> PetNames
        {
            get { return _petNames ?? (_petNames = new List<string>()); }
            set { _petNames = value; }
        }

        public int EdgeCount { get; set; }

        public decimal HoldEdgeSeconds { get; set; }

        /// <summary>
        /// should the sub's cock be referred to as a clit
        /// </summary>
        public bool CallCockAClit { get; set; }

        /// <summary>
        /// should the subs balls be referred to as a pussy
        /// </summary>
        public bool CallBallsAPussy { get; set; }

        /// <summary>
        /// sub's eye color
        /// </summary>
        public string EyeColor { get => _eyeColor ?? (_eyeColor = string.Empty); set=> _eyeColor = value; }

        /// <summary>
        /// sub's hair color
        /// </summary>
        public string HairColor { get => _hairColor ?? (_hairColor = string.Empty); set => _hairColor = value; }

        public int WritingTaskMin { get; set; }
        public int WritingTaskMax { get; set; }
        public bool IsCockBeingTortured { get; set; }
        public int CockTortureCount { get; set; }
        public bool AreBallsBeingTortured { get; set; }
        public int BallsTortureCount { get; set; }

        /// <summary>
        /// How many tokens of what denomination does the sub have
        /// </summary>
        public Dictionary<TokenDenomination, int> Purse => _purse ?? (_purse = new Dictionary<TokenDenomination, int>
        {
            {TokenDenomination.Bronze , 0 },
            {TokenDenomination.Silver , 0 },
            {TokenDenomination.Gold , 0 },
        });

        internal SubPersonality Clone()
        {
            return new SubPersonality()
            {
                Age = Age,
                Birthday = Birthday,
                BallsTortureLevel = BallsTortureLevel,
                CockTortureLevel = CockTortureLevel,
                CockSize = CockSize,
                IsCircumsized = IsCircumsized,
                IsCockPierced = IsCockPierced,
                IsStroking = IsStroking,
                IsEdging = IsEdging,
                HoldEdgeSeconds = HoldEdgeSeconds,
                IsOrgasmRestricted = IsOrgasmRestricted,
                InChastity = InChastity,
                Kinks = Kinks.ToList(),
                Name = Name,
                Safeword = Safeword,
                ToyBox = ToyBox.ToList(),
                PetNames = PetNames.ToList(),
                WillBeAllowedToOrgasm = WillBeAllowedToOrgasm,
                IsCockBeingTortured = IsCockBeingTortured,
                CockTortureCount = CockTortureCount,
                AreBallsBeingTortured = AreBallsBeingTortured,
                BallsTortureCount = BallsTortureCount,
                CallBallsAPussy = CallBallsAPussy,
                CallCockAClit = CallCockAClit,
                EdgeCount = EdgeCount,
                EyeColor = EyeColor,
                HairColor = HairColor,
                StrokePace = StrokePace,
                WritingTaskMax = WritingTaskMax,
                WritingTaskMin = WritingTaskMin,
            };
        }

        private Dictionary<TokenDenomination, int> _purse;
        private List<string> _toyBox;
        private List<string> _petNames;
        private List<string> _kinks;
        private string _name;
        private string _eyeColor;
        private string _hairColor;
    }
}
