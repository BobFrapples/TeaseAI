namespace TeaseAI.Common.Data.RiskyPick
{
    /// <summary>
    /// This is a case used to store a number of edges for Risky Pick
    /// </summary>
    public class GameCase
    {
        /// <summary>
        /// Which number is this case
        /// </summary>
        public int CaseNumber { get; private set; }

        /// <summary>
        /// The number of edges in this box
        /// </summary>
        public int Edges { get; private set; }

        public bool IsOpened { get; set; }

        /// <summary>
        /// Return how many edges in the form of X Edges
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Edges.ToString() + ((Edges > 1) ? " Edges" : " Edge");

        /// <summary>
        /// performs a deep copy of GameCase
        /// </summary>
        /// <returns></returns>
        public GameCase Clone()
        {
            var gameCase = Create(CaseNumber, Edges);
            gameCase.IsOpened = IsOpened;
            return gameCase;
        }

        public static GameCase Create(int caseNumber, int edges)
        {
            return new GameCase
            {
                CaseNumber = caseNumber,
                Edges = edges,
                IsOpened = false,
            };
        }
    }
}
