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

        /// <summary>
        /// Return the Domme's response to what the sub said
        /// </summary>
        /// <param name="session"></param>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        Result<MessageProcessedResult> ProcessMessage(Session session, ChatMessage chatMessage);
    }
}
