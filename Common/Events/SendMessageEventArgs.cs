using System;

namespace TeaseAI.Common.Events
{

    /// <summary>
    /// Used to send a message between participants
    /// </summary>
    public class SendMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Chat message to send
        /// </summary>
        public ChatMessage ChatMessage { get; set; }
    }
}
