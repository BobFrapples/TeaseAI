using System;
using TeaseAI.Common;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.MessageProcessors
{
    class RequestTaskMessageProcessor : IMessageProcessor
    {
        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            return !session.Domme.WasGreeted && chatMessage.Message.ToLower().Contains("task");
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
