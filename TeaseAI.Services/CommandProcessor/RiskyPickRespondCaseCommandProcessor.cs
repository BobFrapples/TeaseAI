using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// When this command is executed, it will pause the engine so the user can select a case
    /// </summary>
    [Obsolete("This is on the block to go away.")]
    public class RiskyPickRespondCaseCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public RiskyPickRespondCaseCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.RiskyPickRespondCase);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickRespondCase) && session.GameBoard != null;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.IsScriptPaused = true;

            // This is janky because respond case is a vocabulary word and the script doesn't have a seperate command
            // Proper fix is to nuke this processor and add choose risky pick to the script
            var edgeCount = (workingSession.GameBoard.LastSelectedCase ?? new Common.Data.RiskyPick.GameCase()).ToString();

            OnCommandProcessed(workingSession, edgeCount);
            return Result.Ok(workingSession);
        }
    }
}

