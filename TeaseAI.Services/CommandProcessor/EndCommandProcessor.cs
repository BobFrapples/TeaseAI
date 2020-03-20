using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class EndCommandProcessor : CommandProcessorBase
    {
        public EndCommandProcessor(LineService lineService): base(Keyword.End, lineService)
        {
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (workingSession.IsBeforeTease && workingSession.Sub.IsStroking)
                workingSession.IsBeforeTease = false;
            workingSession.Scripts.Pop();
            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private LineService _lineService;
    }
}
