using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.VocabularyProcessors
{
    public class RandomVocabularyProcessor : BaseVocabularyProcessor
    {
        private readonly LineService _lineService;
        private readonly IRandomNumberService _randomNumberService;

        protected override Dictionary<string, Func<Session, string, string>> Vocabulary { get; set; }

        public RandomVocabularyProcessor(LineService lineService, IRandomNumberService randomNumberService)
        {
            _lineService = lineService;
            _randomNumberService = randomNumberService;

            Vocabulary = new Dictionary<string, Func<Session, string, string>>
            {
                { "#Random(.*)",  (session, line)=> GetRandom(session, line, 1).ToString()  },
                { "#RANDNumberLow", (session, line) => GetRandom(1 * session.Domme.DomLevel, 6 * session.Domme.DomLevel,1).ToString() },
                { "#RANDNumberHigh", (session, line) => GetRandom(5 * session.Domme.DomLevel, 21 * session.Domme.DomLevel,1).ToString() },
                { "#RANDNumber", (session, line) => GetRandom(1 * session.Domme.DomLevel, 11 * session.Domme.DomLevel,1).ToString() },
                { "#RandomRound5(.*)",  (session, line)=> GetRandom(session, line, 5).ToString()  },
                { "#RandomRound10(.*)",  (session, line)=> GetRandom(session, line, 10).ToString()  },
                { "#RandomRound100(.*)",  (session, line)=> GetRandom(session, line, 100).ToString()  },
            };
        }

        private int GetRandom(int minimum, int maximum, int roundTo)
        {
            var number = _randomNumberService.Roll(minimum, maximum);
            return Convert.ToInt32(Math.Round((decimal)(number / roundTo), 1) * roundTo);
        }

        private int GetRandom(Session session, string line, int roundTo)
        {
            var range = _lineService.GetParenData(line, "#Random(").GetResultOrDefault(new List<string>());
            int.TryParse(range[0], out int min);
            int.TryParse(range[1], out int max);
            return GetRandom(min, max, roundTo);
        }
    }
}
