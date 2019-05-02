using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class VocabularyProcessor
    {
        private Dictionary<string, Func<string, Session, string>> _codeVocabulary;

        public VocabularyProcessor(ILineCollectionFilter lineCollectionFilter, LineService lineService)
        {
            _lineCollectionFilter = lineCollectionFilter;
            _lineService = lineService;
            _codeVocabulary = new Dictionary<string, Func<string, Session, string>>
            {
                { "#SubName", (line, session) => line.Replace("#SubName", session.Sub.Name) },
                { "#PetName", (line, session) =>  line.Replace("#PetName", session.Sub.PetNames[new Random().Next(session.Sub.PetNames.Count)])  },
                { "#DommeName", (line, session) => line.Replace("#DommeName", session.Domme.Name)  },
                { "#DomName", (line, session) => line.Replace("#DomName", session.Domme.Name)  },
                { "#GreetSub", (line, session) => line.Replace("#GreetSub",GetGreetingReplacement(DateTime.Now))  },
                { "#EdgeTaunt", (line, session) => line.Replace("#EdgeTaunt", GetEdgeTaunt(session))  },
                { "#GeneralTime", (line, session) => line.Replace("#GeneralTime", GetGeneralTime(DateTime.Now))  },
                { "#Random(\\d,\\d)",  GetRandomString  },
            };
        }

        private string GetRandomString(string line, Session session)
        {
            var resultString = Regex.Match(line, @"#Random(\\d,\\d)").Value;

            var range = _lineService.GetParenData(resultString, "#Random").GetResultOrDefault(new List<string>());
            int.TryParse(range[0], out int min);
            int.TryParse(range[1], out int max);

            return Regex.Replace(line, "#Random(\\d,\\d)",new Random().Next(min,max).ToString());
        }

        /// <summary>
        /// Replace vocabulary words in the string <paramref name="workingLine"/>
        /// </summary>
        /// <param name="session"></param>
        /// <param name="workingLine"></param>
        /// <returns>Mapped vocabulary line *OR* error with failing vocabulary word</returns>
        public string ReplaceVocabulary(Session session, string workingLine)
        {
            var maxCycles = 10;
            while (IsRelevant(workingLine))
            {
                foreach (var key in _codeVocabulary.Keys)
                {
                    if (new Regex(key).IsMatch(workingLine))
                        workingLine = _codeVocabulary[key](workingLine, session);
                }

                var vocabularyWord = FindVocabularyWord(workingLine);
                if (!string.IsNullOrWhiteSpace(vocabularyWord))
                {
                    var replacement = GetReplacement(session, vocabularyWord);
                    workingLine = workingLine.Replace(vocabularyWord, replacement);
                }
                maxCycles--;
                if (maxCycles == 0)
                    return "Unable to map the vocabulary word " + vocabularyWord;
            }
            return workingLine;
        }

        public bool IsRelevant(string line) => line.Contains("#");

        private string FindVocabularyWord(string workingLine)
        {
            return workingLine.Split(' ').FirstOrDefault(l => l.StartsWith("#"));
        }

        private string GetReplacement(Session session, string key)
        {
            if (key == "#")
                return string.Empty;

            // It wasn't a coded vocabulary word, look up in data
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + session.Domme.PersonalityName + "\\Vocabulary\\" + key + ".txt";
            if (!File.Exists(fileName))
                return key;
            var lines = File.ReadAllLines(fileName).ToList();
            lines = _lineCollectionFilter.FilterLines(session, lines);
            return lines[new Random().Next(lines.Count)];
        }

        private string GetEdgeTaunt(Session session)
        {
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + session.Domme.PersonalityName + "\\Stroke\\Edge\\Edge.txt";
            if (!File.Exists(fileName))
                throw new Exception("No Taunt file found");

            var lines = File.ReadAllLines(fileName).ToList();
            lines = _lineCollectionFilter.FilterLines(session, lines);
            return lines[new Random().Next(lines.Count)];
        }

        private bool IsMatch(string tagName, string key)
        {
            return new Regex(tagName + "[^a-zA-Z]*").IsMatch(key);
        }

        private string GetGreetingReplacement(DateTime time)
        {
            if (3 < time.Hour && time.Hour < 12)
                return "#GoodMorningSub";
            if (11 < time.Hour && time.Hour < 18)
                return "#GoodAfternoonSub";
            //if (17 < time.Hour && time.Hour < 4)
            return "#GoodEveningSub";
        }

        private string GetGeneralTime(DateTime time)
        {
            if (time.Hour > 3 && time.Hour < 11)
                return "this morning";
            if (time.Hour > 8 && time.Hour < 18)
                return "today";
            return "tonight";
        }

        private readonly ILineCollectionFilter _lineCollectionFilter;
        private readonly LineService _lineService;

    }
}
