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

        public Result<string> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            OnMessageProcessed(session);
            return Result.Ok(string.Empty);
        }

        private void OnMessageProcessed(Session session)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs() { Session = session });
        }
    }
}
