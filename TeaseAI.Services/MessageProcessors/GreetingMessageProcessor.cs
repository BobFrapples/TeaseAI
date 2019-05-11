using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.MessageProcessors
{
    public class GreetingMessageProcessor : IMessageProcessor
    {

        public GreetingMessageProcessor(ISettingsAccessor settingsAccessor
            , IStringService stringService)
        {
            _settingsAccessor = settingsAccessor;
            _stringService = stringService;
        }

        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            var greetingUsed = _settingsAccessor.GetGreetings()
               .FirstOrDefault(greet => _stringService.WordExists(chatMessage.Message.ToLower(), greet.ToLower()));
            return (!session.Domme.WasGreeted && !string.IsNullOrWhiteSpace(greetingUsed));
        }

        public Result<MessageProcessedResult> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            var greetingUsed = _settingsAccessor.GetGreetings()
               .FirstOrDefault(greet => _stringService.WordExists(chatMessage.Message.ToLower(), greet.ToLower()));

            // Not being greeted, so bump out
            if (greetingUsed == null)
                return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = string.Empty });

            if (session.Domme.RequiresHonorific)
            {
                if (!_stringService.WordExists(chatMessage.Message.ToLower(), session.Domme.Honorific.ToLower()))
                {
                    return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = _stringService.Capitalize(greetingUsed + " what?") });
                }

                if (session.Domme.RequiresHonorificCapitalized
                    && !_stringService.WordExists(chatMessage.Message, _stringService.Capitalize(session.Domme.Honorific)))
                {
                    return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = _stringService.Capitalize("#CapitalizeHonorific") });
                }
            }
            // This is the *only* time we can get away with this
            session.Domme.WasGreeted = true;

            OnMessageProcessed(session);

            return Result.Ok(new MessageProcessedResult { Session = session, MessageBack = string.Empty });
        }

        private void OnMessageProcessed(Session session)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs() { Session = session });
        }

        private readonly ISettingsAccessor _settingsAccessor;
        private readonly IStringService _stringService;
    }
}
