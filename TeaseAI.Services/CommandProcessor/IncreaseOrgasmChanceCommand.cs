using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class IncreaseOrgasmChanceCommand : CommandProcessorBase
    {
        public IncreaseOrgasmChanceCommand(LineService lineService) : base(Keyword.IncreaseOrgasmChance, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (line.Contains(Keyword.IncreaseOrgasmChance))
                workingSession.Domme.AllowsOrgasms++;

            OnCommandProcessed(workingSession);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
