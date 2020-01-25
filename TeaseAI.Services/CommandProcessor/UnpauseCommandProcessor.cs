using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class UnpauseCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public UnpauseCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.Unpause);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.Unpause);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.IsScriptPaused = false;
            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }
    }
}
