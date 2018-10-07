using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class CallCommandProcessor : CommandProcessorBase
    {
        public CallCommandProcessor(IScriptAccessor scriptAccessor, LineService lineService)
        {
            _lineService = lineService;
            _scriptAccessor = scriptAccessor;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.Call);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.Call);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var getOptions = _lineService.GetParenData(line, Keyword.Call);
            if (getOptions.IsFailure)
                return Result.Fail<Session>(getOptions.Error);
            var workingSession = session.Clone();
            Result<Script> newScript = _scriptAccessor.GetScript(workingSession.Domme, getOptions.Value[0]);
            if (newScript.IsFailure)
                return Result.Fail<Session>(newScript.Error);

            if (getOptions.Value.Count == 2)
            {
                var findBookmark = FindBookmark(newScript.Value.Lines, getOptions.Value[1]);
                if (findBookmark.IsFailure)
                    return Result.Fail<Session>(findBookmark.Error);
                newScript.Value.LineNumber = findBookmark.Value;
            }

            workingSession.Scripts.Push(newScript.Value);

            return Result.Ok(workingSession);
        }

        /// <summary>
        /// finds the location of <paramref name="bookmark"/> in the script. 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bookmark">bookmark keyword with parens, (BookmarkName)</param>
        /// <returns></returns>
        private Result<int> FindBookmark(IEnumerable<string> script, string bookmark)
        {
            for (var i = 0; i < script.Count(); i++)
            {
                if (script.ElementAt(i) == bookmark)
                    return Result.Ok(i);
            }
            return Result.Fail<int>("Bookmark " + bookmark + " is not in this script.");
        }

        private LineService _lineService;
        private IScriptAccessor _scriptAccessor;
    }
}
