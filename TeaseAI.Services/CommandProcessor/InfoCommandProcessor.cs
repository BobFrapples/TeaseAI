using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class InfoCommandProcessor : CommandProcessorBase
    {
        public InfoCommandProcessor(LineService lineService) : base(Keyword.Info, lineService)
        {
        }

        public override string DeleteCommandFrom(string line) => IsRelevant(line) ? string.Empty : line;

        public override Result<Session> PerformCommand(Session session, string line) => Result.Ok(session);

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
