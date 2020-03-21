using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// When this command is executed, it will pause the engine so the user can select a case
    /// </summary>
    public class RiskyPickWaitForCaseCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public RiskyPickWaitForCaseCommandProcessor(LineService lineService) : base(Keyword.RiskyPickWaitForCase, lineService)
        {
            _lineService = lineService;
        }

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickWaitForCase) && session.GameBoard != null;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.IsScriptPaused = true;

            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
