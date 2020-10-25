using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class UnpauseVideoCommandProcessor : CommandProcessorBase
    {
        public UnpauseVideoCommandProcessor(LineService lineService) : base(Keyword.UnpauseVideo, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            OnCommandProcessed(session);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
