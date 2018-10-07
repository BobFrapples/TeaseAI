using System;

namespace TeaseAI.Common.Events
{
    public class CommandProcessedEventArgs : EventArgs
    {
        /// <summary>
        /// The current session
        /// </summary>
        public Session Session { get; set; }

        /// <summary>
        /// This is not guaranteed to be set, so.... check it before you use it
        /// </summary>
        public object Parameter { get; set; }

    }
}
