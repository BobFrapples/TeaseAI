using System;
using TeaseAI.Common;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.MessageProcessors
{
    public class SafewordMessageProcessor : IMessageProcessor
    {
        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            return chatMessage.Message.ToLower() == session.Sub.Safeword;
        }

        public Result<MessageProcessedResult> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = string.Empty });
        }

        private void OnMessageProcessed(Session session)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs() { Session = session });
        }
    }
}
