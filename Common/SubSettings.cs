namespace TeaseAI.Common
{
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
    }
}
