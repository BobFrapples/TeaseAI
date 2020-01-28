using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.VocabularyProcessors;

namespace TeaseAI.Services
{
    public class VocabularyProcessor
    {
        private Dictionary<string, Func<string, Session, string>> _codeVocabulary;
        private List<IVocabularyProcessor> _vocabularyProcessors;

        public VocabularyProcessor(ILineCollectionFilter lineCollectionFilter
            , LineService lineService
            , IVocabularyAccessor vocabularyAccessor
            , IImageAccessor imageAccessor
            , IRandomNumberService randomNumberService)
        {
            _lineCollectionFilter = lineCollectionFilter;
            _lineService = lineService;
            _vocabularyAccessor = vocabularyAccessor;
            _vocabularyProcessors = new List<IVocabularyProcessor>
            {
                new ImageVocabularyProcessor(imageAccessor),
                new RandomVocabularyProcessor(lineService, randomNumberService),
            };
            _codeVocabulary = new Dictionary<string, Func<string, Session, string>>
            {

                { "#DommeName", (line, session) => line.Replace("#DommeName", session.Domme.Name)  },
                { "#DomAge", (line, session) => line.Replace("#DomAge", session.Domme.Age.ToString())  },
                { "#DomApathy", (line, session) => line.Replace("#DomApathy", session.Domme.ApathyLevel.ToString())  },
                { "#DomAvgCockMin", (line, session) => line.Replace("#DomAvgCockMin", session.Domme.CockSmallLimit.ToString())  },
                { "#DomAvgCockMax", (line, session) => line.Replace("#DomAvgCockMax", session.Domme.CockBigLimit.ToString())  },
                { "#DomBirthdayMonth", (line, session) => line.Replace("#DomBirthdayMonth", session.Domme.BirthDay.ToString("MMMM")) },
                { "#DomBirthdayDay", (line, session) => line.Replace("#DomBirthdayDay", session.Domme.BirthDay.Day.ToString()) },
                { "#DomCup", (line, session) => line.Replace("#DomCup", session.Domme.CupSize.ToString())  },
                { "#DomEyes", (line, session) => line.Replace("#DomEyes", session.Domme.EyeColor)  },
                { "#DomHair", (line, session) => line.Replace("#DomHair", session.Domme.HairColor)  },
                { "#DomHairLength", (line, session) => line.Replace("#DomHairLength", session.Domme.HairLength.ToString())  },
                { "#DomHonorific", (line, session) => line.Replace("#DomHonorific", session.Domme.Honorific)  },
                { "#DomLargeCockMin", (line, session) => line.Replace("#DomLargeCockMin", (session.Domme.CockBigLimit+1).ToString())  },
                { "#DomLevel", (line, session) => line.Replace("#DomLevel", session.Domme.DomLevel.ToString())  },
                { "#DomMood", (line, session) => line.Replace("#DomMood", session.Domme.MoodLevel.ToString())  },
                { "#DomMoodMax", (line, session) => line.Replace("#DomMoodMax", session.Domme.MoodHappy.ToString())  },
                { "#DomMoodMin", (line, session) => line.Replace("#DomMoodMin", session.Domme.MoodAngry.ToString())  },
                { "#DomName", (line, session) => line.Replace("#DomName", session.Domme.Name)  },
                { "#DomOrgasmRate", (line, session) => line.Replace("#DomOrgasmRate", "allow " +session.Domme.AllowsOrgasms.ToString().ToLower())  },
                { "#DomRuinRate", (line, session) => line.Replace("#DomRuinRate", "ruin " +session.Domme.RuinsOrgasms.ToString().ToLower())  },
                { "#DomSelfAgeMax", (line, session) => line.Replace("#DomSelfAgeMax", session.Domme.AgeOldLimit.ToString())  },
                { "#DomSelfAgeMin", (line, session) => line.Replace("#DomSelfAgeMin", session.Domme.AgeYoungLimit.ToString())  },
                { "#DomSmallCockMax", (line, session) => line.Replace("#DomSmallCockMax", (session.Domme.CockSmallLimit - 1).ToString())  },
                { "#DomSubAgeMax", (line, session) => line.Replace("#DomSubAgeMax", session.Domme.SubAgeOldLimit.ToString())  },
                { "#DomSubAgeMin", (line, session) => line.Replace("#DomSubAgeMin", session.Domme.SubAgeYoungLimit.ToString())  },

                { "#GreetSub", (line, session) => line.Replace("#GreetSub",GetGreetingReplacement(DateTime.Now))  },
                { "#EdgeTaunt", (line, session) => line.Replace("#EdgeTaunt", GetEdgeTaunt(session))  },
                { "#PetName", GetPetName},

                { "#SubName", (line, session) => line.Replace("#SubName", session.Sub.Name) },
                { "#SubAge", (line, session) => line.Replace("#SubAge", session.Sub.Age.ToString()) },
                { "#SubBirthdayMonth", (line, session) => line.Replace("#SubBirthdayMonth", session.Sub.Birthday.ToString("MMMM")) },
                { "#SubBirthdayDay", (line, session) => line.Replace("#SubBirthdayDay", session.Sub.Birthday.Day.ToString()) },
                { "#SubCockSize", (line, session) => line.Replace("#SubCockSize", session.Sub.CockSize.ToString())  },
                { "#SubEyes", (line, session) => line.Replace("#SubEyes", session.Sub.EyeColor)  },
                { "#SubHair", (line, session) => line.Replace("#SubHair", session.Sub.HairColor)  },
                { "#SubWritingTaskMax", (line, session) => line.Replace("#SubWritingTaskMax", session.Sub.WritingTaskMax.ToString())  },
                { "#SubWritingTaskMin", (line, session) => line.Replace("#SubWritingTaskMin", session.Sub.WritingTaskMin.ToString())  },

                { "#ShortName", (line, session) => line.Replace("#ShortName", session.Sub.Name) },
                { "#GlitterContact1", (line, session) => line.Replace("#GlitterContact1", session.Glitter[0].Name) },
                { "#Contact1", (line, session) => line.Replace("#Contact1", session.Glitter[0].Name) },
                { "#GlitterContact2", (line, session) => line.Replace("#GlitterContact2", session.Glitter[1].Name) },
                { "#Contact2", (line, session) => line.Replace("#Contact2", session.Glitter[1].Name) },
                { "#GlitterContact3", (line, session) => line.Replace("#GlitterContact3", session.Glitter[2].Name) },
                { "#Contact3", (line, session) => line.Replace("#Contact3", session.Glitter[2].Name) },
                { "#GlitterContact4", (line, session) => line.Replace("#GlitterContact4", session.Glitter[3].Name) },
                { "#Contact4", (line, session) => line.Replace("#Contact4", session.Glitter[3].Name) },

                //{ "#CBTCockCount", (line, session) => line.Replace("#CBTCockCount", session.CbtCockCount) },
                //{ "#CBTBallsCount", (line, session) => line.Replace("#CBTBallsCount", session.CbtBallsCount) },

                //{ "#OrgasmLockDate",  (line, session) => session.Sub.IsOrgasmRestricted ? line.Replace("#OrgasmLockDate", "#CockToClit") :"#Cock" },
                { "#Cock",  (line, session) => line.Replace("#Cock", session.Sub.CallCockAClit ? "#CockToClit" :"#Cock" )},
                { "stroking",  (line, session) => session.Sub.CallCockAClit ? line.Replace("stroking", "#StrokingToRubbing") :"stroking" },
                { "#Balls",  (line, session) => session.Sub.CallBallsAPussy ? line.Replace("#Balls", "#BallsToPussy") :"#Balls" },
                { "those #Balls",  (line, session) => session.Sub.CallBallsAPussy ? line.Replace("those #Balls", "that #BallsToPussy") :"those #Balls" },
                { "#SessionEdges", (line, session) => line.Replace("#SessionEdges", session.Sub.EdgeCount.ToString()) },
                // Not sure if this is torture level or how many times it's been done
                { "#SessionCBTCock", (line, session) => line.Replace("#SessionCBTCock", session.Sub.CockTortureLevel.ToString()) },
                { "#SessionCBTBalls", (line, session) => line.Replace("#SessionCBTBalls", session.Sub.BallsTortureLevel.ToString()) },
                { "#GeneralTime", (line, session) => line.Replace("#GeneralTime", GetGeneralTime(DateTime.Now))  },
                { "#CurrentTime", (line, session) => line.Replace("#CurrentTime", DateTime.Now.ToString("h:mm")) },
                { "#CurrentDay", (line, session) => line.Replace("#CurrentDay", DateTime.Now.ToString("dddd")) },
                { "#CurrentMonth", (line, session) => line.Replace("#CurrentMonth", DateTime.Now.ToString("MMMMM")) },
                { "#CurrentYear", (line, session) => line.Replace("#CurrentYear", DateTime.Now.ToString("yyyy")) },
                { "#CurrentDate", (line, session) => line.Replace("#CurrentDate", DateTime.Now.ToShortDateString()) },
                { "#RP_CaseNumber", (line, session) => line.Replace("#RP_CaseNumber", session.GameBoard.SelectedCases.LastOrDefault().ToString()) },
                { "#RP_EdgeOffer", (line, session) => line.Replace("#RP_EdgeOffer", (session.GameBoard.Offer?.Edges).GetValueOrDefault().ToString()) },
                { "#RP_TokenOffer", (line, session) => line.Replace("#RP_TokenOffer", (session.GameBoard.Offer?.Tokens).GetValueOrDefault().ToString()) },
                //{ "#RP_RespondCase", (line, session) => line.Replace("#RP_RespondCase", session.GameBoard.LastSelectedCase ?? string.Empty) },
            };
        }

        private string GetPetName(string line, Session session)
        {
            var petNameIndex = new Random().Next(2, 7);
            if (session.Domme.MoodLevel < session.Domme.MoodAngry)
                petNameIndex = new Random().Next(7, 9);
            if (session.Domme.MoodLevel > session.Domme.MoodHappy)
                petNameIndex = new Random().Next(0, 2);

            return line.Replace("#PetName", session.Sub.PetNames[petNameIndex]);
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
                    var words = workingLine.Split(' ');
                    for (var i = 0; i < words.Length; i++)
                    {
                        if (Regex.IsMatch(words[i], key))
                            words[i] = _codeVocabulary[key](words[i], session);
                    }
                    workingLine = string.Join(" ", words);
                }

                foreach (var processor in _vocabularyProcessors)
                {
                    if (processor.IsRelevant(workingLine))
                    {
                        workingLine = processor.ReplaceVocabulary(session, workingLine);
                    }
                }

                var vocabularyWord = FindVocabularyWord(workingLine);
                if (!string.IsNullOrWhiteSpace(vocabularyWord))
                {
                    var replacement = GetReplacement(session, vocabularyWord);
                    workingLine = workingLine.Replace(vocabularyWord, replacement);
                }
                maxCycles--;
                // We can't figure it out. Show it to the user
                if (maxCycles == 0)
                    return workingLine;
                //return "Unable to map the vocabulary word " + vocabularyWord;
            }
            return workingLine;
        }

        public bool IsRelevant(string line) => !string.IsNullOrWhiteSpace(FindVocabularyWord(line));

        private string FindVocabularyWord(string workingLine)
        {
            return workingLine.Split(' ').FirstOrDefault(l => l.StartsWith("#"));
        }

        private string GetReplacement(Session session, string key)
        {
            if (key == "#")
                return string.Empty;
            var getVocabulary = _vocabularyAccessor.GetVocabulary(session.Domme, key)
                .OnSuccess(lines => _lineCollectionFilter.FilterLines(session, lines))
                .OnSuccess(lines => lines[new Random().Next(lines.Count)]);
            return getVocabulary.GetResultOrDefault(key);
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
        private readonly IVocabularyAccessor _vocabularyAccessor;
    }
}
