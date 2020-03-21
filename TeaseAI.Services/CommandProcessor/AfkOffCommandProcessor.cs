using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class AfkOffCommandProcessor : CommandProcessorBase
    {
        public AfkOffCommandProcessor(LineService lineService) : base(Keyword.AfkOff, lineService)
        {
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (session.Domme.IsAfk)
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Domme.IsAfk = true;

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script,string personalityName, string line) => Result.Ok();

        private LineService _lineService;
    }
}
