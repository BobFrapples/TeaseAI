using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class EndCommandProcessor : ICommandProcessor
    {
        public EndCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string input) => _lineService.DeleteCommand(input, Keyword.End);

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.End);

        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (workingSession.IsBeforeTease && workingSession.Sub.IsStroking)
                workingSession.IsBeforeTease = false;
            workingSession.Scripts.Pop();
            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        private void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        private LineService _lineService;

    }
}
