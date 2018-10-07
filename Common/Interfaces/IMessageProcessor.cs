using System;
using TeaseAI.Common.Events;

namespace TeaseAI.Common.Interfaces
{
    public interface IMessageProcessor
    {
        /// <summary>
        /// Event fired if the proccessor successfully handles this events
        /// </summary>
        event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        bool IsRelevant(Session session, ChatMessage chatMessage);

        Result<string> ProcessMessage(Session session, ChatMessage chatMessage);
    }
}
