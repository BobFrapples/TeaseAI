using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services
{
    public class VocabularyProcessor
    {
        public VocabularyProcessor()
        {
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
                var vocabularyWord = workingLine.Split(' ').First(l => l.StartsWith("#"));
                var replacement = GetReplacement(session, vocabularyWord);

                workingLine = workingLine.Replace(vocabularyWord, replacement);
                maxCycles--;
                if (maxCycles == 0)
                    return "Unable to map the vocabulary word " + vocabularyWord;
            }
            return workingLine;
        }

        public bool IsRelevant(string line) => line.Contains("#");

        private string GetReplacement(Session session, string key)
        {
            if (key == "#")
                return string.Empty;
            else if (IsMatch("#SubName", key))
                return key.Replace("#SubName", session.Sub.Name);
            // TODO: Set #PetName based upon domme mood
            else if (IsMatch("#PetName", key))
                return key.Replace("#PetName", session.Sub.PetNames[new Random().Next(session.Sub.PetNames.Count)]);
            else if (IsMatch("#DommeName", key))
                return key.Replace("#DommeName", session.Domme.Name);
            else if (IsMatch("#DomName", key))
                return key.Replace("#DomName", session.Domme.Name);
            else if (IsMatch("#GreetSub", key))
                return GetGreetingReplacement(DateTime.Now);

            // It wasn't a coded vocabulary word, look up in data
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + session.Domme.PersonalityName + "\\Vocabulary\\" + key + ".txt";
            if (!File.Exists(fileName))
                return key;
            var lines = File.ReadAllLines(fileName).ToList();

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
    }
}
