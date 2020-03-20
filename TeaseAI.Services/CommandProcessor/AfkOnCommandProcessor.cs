using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class AfkOnCommandProcessor : CommandProcessorBase
    {
        public AfkOnCommandProcessor(LineService lineService) : base(Keyword.AfkOn, lineService)
        {
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

        protected override Result ParseCommandSpecific(Script script, string personalityhName, string line) => Result.Ok();

    }
}
