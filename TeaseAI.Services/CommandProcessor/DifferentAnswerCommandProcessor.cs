using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class DifferentAnswerCommandProcessor : CommandProcessorBase
    {
        public DifferentAnswerCommandProcessor(LineService lineService) : base(Keyword.DifferentAnswer, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line) => Result.Ok(session);

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return Result.Ok()
                .Ensure(() => line.StartsWith(Keyword.DifferentAnswer), Keyword.DifferentAnswer + " must at the start of the line.");
        }
    }
}
