using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class RapidCodeOnCommandProcessor : CommandProcessorBase
    {

        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.RapidCodeOn, string.Empty).Replace(Keyword.RapidTextOn, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RapidCodeOn) || line.Contains(Keyword.RapidTextOn);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var newSession = session.Clone();
            if (IsRelevant(newSession, line))
                newSession.Domme.MessageTimer = 1;
            return Result.Ok(newSession);
        }
    }
}
