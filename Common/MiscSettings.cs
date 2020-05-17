namespace TeaseAI.Common
{
    /// <summary>
    /// Settings on the Miscelaneous tab
    /// </summary>
    public class MiscSettings
    {
        public MiscSettings()
        {
            Version = 1;
        }
        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Is the sub in chastity
        /// </summary>
        public bool IsInChastity { get; set; }
        /// <summary>
        /// Is the program in offline mode
        /// </summary>
        public bool IsOffline { get; set; }
    }
}
