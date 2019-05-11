using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.MessageProcessors
{
    public class ScriptResponseMessageProcessor : IMessageProcessor
    {

        public ScriptResponseMessageProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public event EventHandler<MessageProcessedEventArgs> MessageProcessed;

        public bool IsRelevant(Session session, ChatMessage chatMessage)
        {
            if (session.CurrentScript == null)
                return false;

            return session.CurrentScript.CurrentLine.StartsWith("[");
        }

        public Result<MessageProcessedResult> ProcessMessage(Session session, ChatMessage chatMessage)
        {
            var _stringService = new StringService();
            var workingSession = session.Clone();
            var lineNumer = workingSession.CurrentScript.LineNumber;

            var expectedResponses = new List<Response>();
            while (workingSession.CurrentScript.CurrentLine.StartsWith("["))
            {
                var workingLine = workingSession.CurrentScript.CurrentLine;
                var keywords = _lineService.GetParenData(workingLine, "[");
                if (keywords.IsFailure)
                    return Result.Fail<MessageProcessedResult>(keywords.Error);

                var response = new Response(string.Empty);
                response.Phrases.AddRange(keywords.Value);
                var command = _lineService.DeleteCommand(workingLine, "[");
                response.Responses.Add(Response.Script, new List<string>() { command });
                expectedResponses.Add(response);

                workingSession.CurrentScript.LineNumber++;
            }

            var foundResponse = FindMatchingResponse(chatMessage, expectedResponses);

            if (foundResponse == null)
            {
                var isDifferentAnswer = GetCodedAnswer(workingSession, Keyword.DifferentAnswer)
                    .OnSuccess(resp =>
                    {
                        //workingSession.CurrentScript.LineNumber++;
                        return new MessageProcessedResult { Session = session, MessageBack = resp };
                    });

                if (isDifferentAnswer.IsSuccess)
                    return isDifferentAnswer;

                var isAcceptAnswer = GetCodedAnswer(workingSession, Keyword.AcceptAnswer)
                    .OnSuccess(resp =>
                    {
                        workingSession.CurrentScript.LineNumber++;
                        return new MessageProcessedResult { Session = workingSession, MessageBack = resp };
                    }); ;

                if (isAcceptAnswer.IsSuccess)
                    return isAcceptAnswer;

                return Result.Fail<MessageProcessedResult>("Unknown response and no @DifferentAnswer or @AcceptAnswer tag set");
            }
            else
            {
                var phrase = foundResponse.Phrases.FirstOrDefault(phr => phr.ToLower() == "yes");
                if (phrase == null)
                    phrase = foundResponse.Phrases.FirstOrDefault(phr => phr.ToLower() == "no");

                if (phrase != null)
                {
                    if (workingSession.Domme.RequiresHonorific)
                    {
                        if (!_stringService.WordExists(chatMessage.Message.ToUpper(), workingSession.Domme.Honorific.ToUpper()))
                            return Result.Ok(new MessageProcessedResult { Session = workingSession, MessageBack = _stringService.Capitalize(phrase + " what?") });

                        if (workingSession.Domme.RequiresHonorificCapitalized
                            && !_stringService.WordExists(chatMessage.Message, _stringService.Capitalize(workingSession.Domme.Honorific.ToLower())))
                            return Result.Ok(new MessageProcessedResult { Session = workingSession, MessageBack = "#CapitalizeHonorific" });
                    }
                }
                while (workingSession.CurrentScript.CurrentLine.StartsWith(Keyword.AcceptAnswer) ||
                    workingSession.CurrentScript.CurrentLine.StartsWith(Keyword.DifferentAnswer))
                    workingSession.CurrentScript.LineNumber++;

                OnMessageProcessed(workingSession, foundResponse);
            }

            // If Everything went well, the event handler will finish processing any commands
            return Result.Ok(new MessageProcessedResult { Session = workingSession, MessageBack = null });
        }

        Result<string> GetCodedAnswer(Session session, string command)
        {
            return Result.Ok(session.CurrentScript.CurrentLine)
                .Ensure(line => line.StartsWith(command), "Not " + command)
                .OnSuccess(line => line.Replace(command, string.Empty));
        }

        private Response FindMatchingResponse(ChatMessage chatMessage, List<Response> expectedResponses)
        {
            foreach (var searchResponse in expectedResponses)
            {
                if (searchResponse.Phrases.Any(phr => chatMessage.Message.ToLower().Contains(phr.ToLower())))
                    return searchResponse;
            }
            return null;
        }

        private void OnMessageProcessed(Session session, Response response)
        {
            MessageProcessed?.Invoke(this, new MessageProcessedEventArgs() { Session = session, Parameter = response });
        }

        private readonly LineService _lineService;
    }
}
