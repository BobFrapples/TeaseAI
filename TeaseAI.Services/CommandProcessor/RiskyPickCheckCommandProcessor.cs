using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data.RiskyPick;

namespace TeaseAI.Services.CommandProcessor
{
    public class RiskyPickCheckCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public RiskyPickCheckCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.RiskyPickCheck);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickCheck) && session.GameBoard?.PlayersCase != null;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.GameBoard.Offer = GetOffer(workingSession.GameBoard);

            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        private RiskyPickOffer GetOffer(RiskyPickGameBoard gameBoard)
        {
            var closedCases = gameBoard.Cases.Values.Where(c => !c.IsOpened).ToList();
            var edgeCount = closedCases.Sum(c => c.Edges);
            var tokenCount = closedCases.Sum(c => RiskyPickGameBoard.TokensNumerator / c.Edges);
            var offerAverage = closedCases.Count();
            return new RiskyPickOffer
            {
                Edges = (int)Math.Ceiling((decimal)edgeCount / offerAverage),
                Tokens = (int)Math.Ceiling((decimal)tokenCount / offerAverage),
                HighestRisk = closedCases.Max(c => c.Edges),
                LowestRisk = closedCases.Min(c => c.Edges),
            };
        }
    }
}
