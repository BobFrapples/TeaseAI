using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.CommandProcessor
{
    public class RiskyPickSelectCaseCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public RiskyPickSelectCaseCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.RiskyPickSelectCase);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickSelectCase) && session.GameBoard != null;


        public override Result<Session> PerformCommand(Session session, string line)
        {
            return _lineService.GetParenData(line, Keyword.RiskyPickSelectCase)
                .Ensure(l => l.Count == 1, "Too many parameters passed to " + Keyword.RiskyPickSelectCase + " in " + line)
                .OnSuccess(l => Convert.ToInt16(l[0]))
                .OnSuccess(cn =>
                {
                    if (session.GameBoard.Cases[cn].IsOpened)
                        Result.Fail<int>("Case number " + cn.ToString() + " is already opened.");
                    return cn;
                })
                .OnSuccess(cn =>
                {
                    var workingSession = session.Clone();

                    if (workingSession.GameBoard.CurrentRound == 0)
                        workingSession.GameBoard.PlayersCase = workingSession.GameBoard.Cases[cn];
                    else
                    {
                        workingSession.GameBoard.Cases[cn].IsOpened = true;
                        workingSession.GameBoard.SelectedCases.Add(cn);
                    }
                    OnCommandProcessed(workingSession);

                    return workingSession;
                });
        }
    }
}
