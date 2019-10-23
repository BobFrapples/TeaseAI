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

        public string Name { get; set; }

        /// <summary>
        /// List of things the sub likes. Will never be null(Nothing in VB)
        /// </summary>
        public List<string> Kinks
        {
            get { return _kinks ?? (_kinks = new List<string>()); }
            set { _kinks = value; }
        }
        private List<string> _kinks;

        /// <summary>
        /// Lisst of toys the sub owns. Will never be null(Nothing in VB)
        /// </summary>
        public List<string> ToyBox
        {
            get { return _toyBox ?? (_toyBox = new List<string>()); }
            set { _toyBox = value; }
        }

        public string Safeword { get; set; }
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
        public bool CallCockAClit { get; set; }
        public bool CallBallsAPussy { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public int WritingTaskMin { get; set; }
        public int WritingTaskMax { get; set; }
        public bool IsCockBeingTortured { get; set; }
        public int CockTortureCount { get; set; }

        private List<string> _toyBox;
        private List<string> _petNames;

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
    }
}
