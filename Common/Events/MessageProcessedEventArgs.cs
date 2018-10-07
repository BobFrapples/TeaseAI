using System;

namespace TeaseAI.Common.Events
{
    public class MessageProcessedEventArgs : EventArgs
    {
        public Session Session { get; set; }
        /// <summary>
        /// subscriber needs to figure this out.
        /// </summary>
        public object Parameter { get; set; }
    }
}
