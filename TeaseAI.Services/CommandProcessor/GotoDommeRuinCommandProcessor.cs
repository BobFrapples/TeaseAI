using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class GotoDommeRuinCommandProcessor : ICommandProcessor
    {
        public GotoDommeRuinCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string input) => _lineService.DeleteCommand(input, Keyword.GotoDommeRuin);

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.GotoDommeRuin);

        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var result = FindBookmark(workingSession.CurrentScript.Lines.ToList(), "(" + workingSession.Domme.RuinsOrgasms.ToString() + " Ruins)")
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
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

        private void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        private LineService _lineService;
    }
}
