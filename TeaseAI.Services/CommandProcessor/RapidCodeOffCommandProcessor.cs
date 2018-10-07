using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class RapidCodeOffCommandProcessor : CommandProcessorBase
    {
        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.RapidCodeOff, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RapidCodeOff);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var newSession = session.Clone();
            if (IsRelevant(newSession, line))
                newSession.Domme.MessageTimer = 2000;
            return Result.Ok(newSession);
        }
    }
}
