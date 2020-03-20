using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class NullResponseCommandProcessor : CommandProcessorBase
    {
        public NullResponseCommandProcessor(LineService lineService) : base(Keyword.NullResponse, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line) => Result.Ok(session);

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
