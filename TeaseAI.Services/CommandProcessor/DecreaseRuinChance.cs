using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class DecreaseRuinChanceCommand : CommandProcessorBase
    {
        public DecreaseRuinChanceCommand(LineService lineService) : base(Keyword.DecreaseRuinChance, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (line.Contains(Keyword.DecreaseRuinChance))
            {
                workingSession.Domme.RuinsOrgasms--;
            }

            OnCommandProcessed(workingSession);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
