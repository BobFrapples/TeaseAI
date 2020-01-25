namespace TeaseAI.Common.Data.RiskyPick
{
    public class RiskyPickOffer
    {
        public int Tokens { get; set; }
        public int Edges { get; set; }
        public int HighestRisk { get; set; }
        public int LowestRisk { get; set; }

        public RiskyPickOffer Clone()
        {
            return new RiskyPickOffer
            {
                Tokens = Tokens,
                Edges = Edges,
                HighestRisk = HighestRisk,
                LowestRisk = LowestRisk,
            };
        }
    }
}
