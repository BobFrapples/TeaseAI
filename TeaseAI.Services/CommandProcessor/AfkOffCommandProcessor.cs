using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class AfkOffCommandProcessor : CommandProcessorBase
    {
        public AfkOffCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.AfkOff);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.AfkOn);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (session.Domme.IsAfk)
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Domme.IsAfk = true;

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        private LineService _lineService;
    }
}
