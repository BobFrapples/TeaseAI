using System;

namespace TeaseAI.Common
{
    /// <summary>
    /// Message sent from one person to another, used to update the chat window
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChatMessage()
        {
            TimeStamp = DateTime.Now;
        }

        /// <summary>
        /// message sent
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// message sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
