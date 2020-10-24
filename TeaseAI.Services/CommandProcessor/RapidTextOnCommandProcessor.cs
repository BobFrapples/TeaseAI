using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class RapidTextOnCommandProcessor : CommandProcessorBase
    {
        public RapidTextOnCommandProcessor(LineService lineService) : base(Keyword.RapidTextOn, lineService)
        {
        }

        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.RapidCodeOn, string.Empty).Replace(Keyword.RapidTextOn, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RapidCodeOn) || line.Contains(Keyword.RapidTextOn);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (IsRelevant(workingSession, line))
                workingSession.Domme.MessageTimer = 1;
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            if (line.Contains(Keyword.RapidCodeOn))
                return Result.Fail(Keyword.RapidCodeOn + " is deprecated, please use " + Keyword.RapidTextOn);

            return Result.Ok();
        }
    }
}
