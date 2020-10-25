using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class UnpauseCommandProcessor : CommandProcessorBase
    {
        public UnpauseCommandProcessor(LineService lineService): base(Keyword.Unpause, lineService)
        {
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.IsScriptPaused = false;
            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
