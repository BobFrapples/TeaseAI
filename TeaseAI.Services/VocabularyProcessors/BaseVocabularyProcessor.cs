using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.VocabularyProcessors
{
    public abstract class BaseVocabularyProcessor : IVocabularyProcessor
    {
        protected abstract Dictionary<string, Func<Session, string, string>> Vocabulary { get; set; }

        public string ReplaceVocabulary(Session session, string workingLine)
        {
            if (IsRelevant(workingLine))
            {
                foreach (var key in Vocabulary.Keys)
                {
                    var words = workingLine.Split(' ');
                    for (var i = 0; i < words.Length; i++)
                    {
                        if (Regex.IsMatch(words[i], key))
                        {
                            words[i] = Regex.Replace(words[i], key, Vocabulary[key](session, words[i]));
                        }
                    }
                    workingLine = string.Join(" ", words);
                }
            }
            return workingLine;
        }


        public bool IsRelevant(string line) => !string.IsNullOrWhiteSpace(FindVocabularyWord(line));

        private string FindVocabularyWord(string workingLine)
        {
            return workingLine.Split(' ').FirstOrDefault(l => l.StartsWith("#"));
        }
    }
}
