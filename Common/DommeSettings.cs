using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Settings specific to the Domme
    /// </summary>
    public class DommeSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DommeSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// How caring does the Domme start each session
        /// </summary>
        public ApathyLevel ApathyLevel { get; set; }
    }
}
