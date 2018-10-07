using System;

namespace TeaseAI.Common.Events
{
    public class DommeSaidEventArgs : EventArgs
    {
        public ChatMessage ChatMessage { get; set; }
    }
}
