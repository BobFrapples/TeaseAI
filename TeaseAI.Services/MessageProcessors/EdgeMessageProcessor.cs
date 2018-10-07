using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.MessageProcessors
{
    public class EdgeMessageProcessor : IMessageProcessor
    {

        public EdgeMessageProcessor(ISystemVocabularyAccessor systemVocabularyAccessor)
        {
            _systemVocabularyAccessor = systemVocabularyAccessor;
        }

        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            var data = _systemVocabularyAccessor.GetData(session, "EdgeKEY")
                .OnSuccess(list => list.Any(line => line.ToLower() == Normalize(chatMessage.Message)));

            return data.Value;
        }

        public Result<string> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            throw new NotImplementedException();
        }

        private void OnMessageProcesses(Session session)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs()
            {
                Session = session,
            });
        }

        private string Normalize(string message)
        {
            return message
                .Replace("'", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("!", "")
                .Replace("  ", " ");
        }

        private readonly ISystemVocabularyAccessor _systemVocabularyAccessor;
    }
}
