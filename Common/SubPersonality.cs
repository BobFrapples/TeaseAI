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
        public bool IsStroking { get; set; }

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

        [Obsolete("Find out  what this is for and document")]
        public bool IsAlreadyStrokingEdge { get; set; }

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
                IsOrgasmRestricted = IsOrgasmRestricted,
                InChastity = InChastity,
                Kinks = Kinks.ToList(),
                Name = Name,
                Safeword = Safeword,
                ToyBox = ToyBox.ToList(),
                PetNames = PetNames.ToList(),
                WillBeAllowedToOrgasm = WillBeAllowedToOrgasm,
            };
        }
    }
}
