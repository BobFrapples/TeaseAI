namespace TeaseAI.Common
{
    /// <summary>
    /// Settings which are specific to the UI in question
    /// </summary>
    public class UiSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UiSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Show the timestamp in chat window
        /// </summary>
        public bool IsTimeStampEnabled { get; set; }
    }
}
