using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common
{
    /// <summary>
    /// Settings on the general chat tab
    /// </summary>
    public class GeneralSettings
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralSettings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Can the Domme actually delete files not owned by this program
        /// </summary>
        public bool CanDommeDeleteFiles { get; set; }
        
        /// <summary>
        /// Show the timestamp in chat window
        /// </summary>
        public bool IsTimeStampEnabled { get; set; }

        /// <summary>
        /// Show the username of chat message senders in the UI
        /// </summary>
        public bool ShowChatUserNames { get; set; }

        /// <summary>
        /// Control whether or not Domme messages are instant or have a "typing" delay
        /// </summary>
        public bool DoesDommeTypeInstantly { get; set; }

        /// <summary>
        /// Should the tease work like a web tease from milovana
        /// </summary>
        public bool IsWebTeaseModeEnabled { get; set; }
    }
}
