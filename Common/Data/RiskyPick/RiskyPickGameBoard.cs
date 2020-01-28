using System;
using System.Collections.Generic;
using System.Linq;

namespace TeaseAI.Common.Data.RiskyPick
{
    public class RiskyPickGameBoard
    {
        #region Constants
        public const int NumberOfCases = 24;

        /// <summary>
        /// This defines that maximum winnings you can get from the game. 
        /// Winnings = TokensNumerator / PlayerCase.Edges
        /// Therefore, maximum is 1000 tokens.
        /// </summary>
        public const int TokensNumerator = 1000;

        /// <summary>
        /// How many cases to pick in each round.
        /// Round 0 is one case (This is the players case)
        /// Round 1 is six cases, etc.
        /// </summary>
        public List<int> CasesToPick { get; private set; } = new List<int> { 1, 6, 6, 3, 3, 2, 2 };
        #endregion

        public Dictionary<int, GameCase> Cases => _cases ?? (_cases = new Dictionary<int, GameCase>());

        public GameCase PlayersCase { get; set; }

        public GameCase LastSelectedCase => GetLastSelectedCase();

        public int CurrentRound { get; set; }

        public List<int> SelectedCases => _selectedCases ?? (_selectedCases = new List<int>());

        public RiskyPickOffer Offer { get; set; }

        public RiskyPickGameBoard Clone()
        {
            var newBoard = new RiskyPickGameBoard
            {
                Offer = Offer?.Clone(),
                CurrentRound = CurrentRound,
                _selectedCases = _selectedCases,
            };
            foreach (var i in Cases.Keys)
                newBoard.Cases[i] = Cases[i].Clone();
            if (PlayersCase != null)
                newBoard.PlayersCase = newBoard.Cases[PlayersCase.CaseNumber];
            return newBoard;
        }

        public static RiskyPickGameBoard Create(List<int> caseValues)
        {
            if (NumberOfCases != caseValues.Count)
                throw new ArgumentOutOfRangeException("You must have 24 cases in Risky Pick");
            var newBoard = new RiskyPickGameBoard();
            var i = 0;
            var cases = caseValues
                .OrderBy(cv => Guid.NewGuid())
                .Select(e => GameCase.Create(++i, e))
                .ToList();
            cases.ForEach(c => newBoard.Cases[c.CaseNumber] = c);
            return newBoard;
        }

        private GameCase GetLastSelectedCase()
        {
            var selCase = SelectedCases.LastOrDefault();
            if (selCase == 0)
                return PlayersCase;
            return Cases[selCase];
        }

        private Dictionary<int, GameCase> _cases;
        private List<int> _selectedCases;
    }
}
