using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    ///<summary>
    /// The @Chance Command gives a chance to either jump to the line specified, or move to the next line as normal. The odds of jumping to the specified line are indicated in the Command
    /// itself. For example, @Chance50(Domme Instructions) would have a 50% chance of jumping to (Domme Instructions).
    ///</summary>
    public class ChanceCommandProcessor : ICommandProcessor
    {
        public ChanceCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string input)
        {
            if (!input.Contains(Keyword.Chance))
                return input;
            var chance = input.Substring(input.IndexOf(Keyword.Chance) + Keyword.Chance.Length, 2);

            return _lineService.DeleteCommand(input, Keyword.Chance + chance + "(");
        }

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.Chance);

        public Result<Session> PerformCommand(Session session, string line)
        {
            var chance = line.Substring(line.IndexOf(Keyword.Chance) + Keyword.Chance.Length, 2);
            var chanceNum = 0;
            if (!int.TryParse(chance, out chanceNum))
                throw new Exception("Unable to determine chance percent");

            var jumpRoll = new Random().Next(100);
            if (jumpRoll <= chanceNum)
            {
                var workingSession = session.Clone();
                var result = _lineService.GetParenData(line, Keyword.Chance + chance + "(")
                    .OnSuccess(pd => "(" + pd[new Random().Next(pd.Count)] + ")")
                    .OnSuccess(bm => FindBookmark(workingSession.CurrentScript.Lines.ToList(), bm))
                    .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                    .Map(ln => workingSession)
                    .OnSuccess(sesh => OnCommandProcessed(sesh));
                return result;
            }
            return Result.Ok(session);
        }

        /// <summary>
        /// finds the location of <paramref name="bookmark"/> in the script. 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bookmark">bookmark keyword with parens, (BookmarkName)</param>
        /// <returns></returns>
        private Result<int> FindBookmark(List<string> script, string bookmark)
        {
            for (var i = 0; i < script.Count; i++)
            {
                if (script[i] == bookmark)
                    return Result.Ok(i);
            }
            return Result.Fail<int>("Bookmark " + bookmark + " is not in this script.");
        }

        void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        LineService _lineService;
    }
}
