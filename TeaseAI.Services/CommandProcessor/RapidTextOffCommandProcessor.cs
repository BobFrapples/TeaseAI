using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class RapidTextOffCommandProcessor : CommandProcessorBase
    {
        public RapidTextOffCommandProcessor(LineService lineService) : base(Keyword.RapidTextOff, lineService)
        {
        }

        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.RapidCodeOff, string.Empty).Replace(Keyword.RapidTextOff, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RapidCodeOff) || line.Contains(Keyword.RapidTextOff);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var newSession = session.Clone();
            if (IsRelevant(newSession, line))
                newSession.Domme.MessageTimer = 1000;
            return Result.Ok(newSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            if (line.Contains(Keyword.RapidCodeOff))
                return Result.Fail(Keyword.RapidCodeOff + " is deprecated, please use " + Keyword.RapidTextOff);

            return Result.Ok();
        }
    }
}
