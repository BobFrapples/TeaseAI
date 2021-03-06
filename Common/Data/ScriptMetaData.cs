﻿using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// Information about scripts, but not the actual script or where the domme is in the script
    /// </summary>
    public class ScriptMetaData
    {
        /// <summary>
        /// Name of the current script
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Key to find the script, currently the fill path to the file
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Description or information about the script
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Is this script enabled or not
        /// </summary>
        public bool IsEnabled { get; set; }
        public SessionPhase SessionPhase { get; set; }
        /// <summary>
        /// This script is meant for subs in chastity
        /// </summary>
        public bool IsChastity { get; set; }
        /// <summary>
        /// This script is meant for subs that have restricted orgasms
        /// </summary>
        public bool IsRestricted { get; set; }
        /// <summary>
        /// Unsure
        /// </summary>
        public bool IsBeg { get; set; }
        /// <summary>
        /// Unsure
        /// </summary>
        public bool IsEdge { get; set; }

        public ScriptMetaData Clone()
        {
            return new ScriptMetaData()
            {
                Name = Name,
                Key = Key,
                Info = Info,
                IsEnabled = IsEnabled,
            };
        }
    }
}
