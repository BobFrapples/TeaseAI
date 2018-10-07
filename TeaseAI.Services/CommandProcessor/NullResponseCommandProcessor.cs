using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class NullResponseCommandProcessor : CommandProcessorBase
    {
        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.NullResponse, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.NullResponse);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            return Result.Ok(session);
        }
    }
}
