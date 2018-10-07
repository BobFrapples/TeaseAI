using System;

namespace TeaseAI.Common
{
    public class ChatMessage
    {
        public ChatMessage()
        {
            TimeStamp = DateTime.Now;
        }
        
        public string Message { get; set; }
        public string Sender { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
