using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class CockTortureCommandProcessor : CommandProcessorBase
    {
        private LineService _lineService;

        public CockTortureCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.CockTorture);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.CockTorture);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (!session.Sub.Kinks.Contains(Kink.CockTorture))
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Sub.IsCockBeingTortured = true;

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }
    }
}
