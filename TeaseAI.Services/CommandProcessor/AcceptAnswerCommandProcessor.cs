using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class AcceptAnswerCommandProcessor : CommandProcessorBase
    {
        public AcceptAnswerCommandProcessor(LineService lineService) : base(Keyword.AcceptAnswer, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line) => Result.Ok(session);

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return Result.Ok()
                .Ensure(() => line.StartsWith(Keyword.AcceptAnswer), Keyword.AcceptAnswer + " must at the start of the line.");
        }
    }
}
