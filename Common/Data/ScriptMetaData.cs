using System;

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
        /// Key to find the script
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Description or information about the script
        /// </summary>
        public string Info { get; set; }

        public ScriptMetaData Clone()
        {
            return new ScriptMetaData()
            {
                Name = Name,
                Key = Key,
                Info = Info,
            };
        }
    }
}
