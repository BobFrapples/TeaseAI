using System.Collections.Generic;

namespace TeaseAI.Common
{
    public class VocabularyResponses
    {
        public string FirstRound = "First Round";
        public string Stroking = "Stroking";
        public string NotStroking = "Not Stroking";
        public string Edging = "Edging";
        public string HoldingTheEdge = "Holding The Edge";
        public string CbtCock = "CBT Cock";
        public string CbtBalls= "CBT Balls";
        public string AfterTease = "After Tease";
        
        public VocabularyResponses()
        {
            TriggerWords = new List<string>();
            Responses = new Dictionary<string, List<string>>();
            Responses[FirstRound] = new List<string>();
            Responses[Stroking] = new List<string>();
            Responses[NotStroking] = new List<string>();
            Responses[Edging] = new List<string>();
            Responses[HoldingTheEdge] = new List<string>();
            Responses[CbtCock] = new List<string>();
            Responses[CbtBalls] = new List<string>();
            Responses[AfterTease] = new List<string>();
        }

        /// <summary>
        /// words from the sub which will trigger a response
        /// </summary>
        List<string> TriggerWords { get; set; }
        Dictionary<string, List<string>> Responses { get; set; }
    }
}
