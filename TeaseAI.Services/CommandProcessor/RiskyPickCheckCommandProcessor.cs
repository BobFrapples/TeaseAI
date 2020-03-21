using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Data.RiskyPick;

namespace TeaseAI.Services.CommandProcessor
{
    public class RiskyPickCheckCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;

        public RiskyPickCheckCommandProcessor(LineService lineService): base(Keyword.RiskyPickCheck, lineService)
        {
            _lineService = lineService;
        }

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickCheck) && session.GameBoard?.PlayersCase != null;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.GameBoard.Offer = GetOffer(workingSession.GameBoard);

            OnCommandProcessed(workingSession);
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

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
