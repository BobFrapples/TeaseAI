using System.Collections.Generic;

namespace TeaseAI.Common.Data
{
    public class Response
    {
        public static string Script => "Script";
        public static string All => "All";
        public static string BeforeTease => "Before Tease";
        public static string FirstRound => "First Round";
        public static string Stroking => "Stroking";
        public static string NotStroking => "Not Stroking";
        public static string Edging => "Edging";
        public static string HoldingTheEdge => "Holding The Edge";
        public static string CbtCock => "CBT Cock";
        public static string CbtBalls => "CBT Balls";
        public static string AfterTease => "After Tease";
        public static string Chastity => "Chastity";

        public string Key { get; private set; }

        /// <summary>
        /// words and phrases that will trigger this response
        /// </summary>
        public List<string> Phrases { get; private set; }

        /// <summary>
        /// possible responses based on where in the session we are
        /// </summary>
        public Dictionary<string, List<string>> Responses { get; private set; }

        public Response(string key)
        {
            Phrases = new List<string>();
            Responses = new Dictionary<string, List<string>>();
            Responses.Add(BeforeTease, new List<string>());
            Responses.Add(FirstRound, new List<string>());
            Responses.Add(Stroking, new List<string>());
            Responses.Add(NotStroking, new List<string>());
            Responses.Add(Edging, new List<string>());
            Responses.Add(HoldingTheEdge, new List<string>());
            Responses.Add(CbtCock, new List<string>());
            Responses.Add(CbtBalls, new List<string>());
            Responses.Add(AfterTease, new List<string>());
            Responses.Add(Chastity, new List<string>());
            Key = key;
        }
    }
}
