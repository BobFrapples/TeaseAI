using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.Processor
{
    public class GotoProcessor
    {

        public GotoProcessor(IScriptAccessor getScripts)
        {
            _getScripts = getScripts;
            _lineService = new LineService();
        }

        public Result<string> GetBookmark(string input)
        {
            var result = _lineService.GetParenData(input, Keyword.Goto)
                 .OnSuccess(pd => "(" + pd[new Random().Next(0, pd.Count)] + ")");
            return result;
        }

        public string DeleteGoto(string input) => _lineService.DeleteCommand(input, Keyword.Goto);

        /// <summary>
        /// finds the location of <paramref name="bookmark"/> in the script. 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bookmark">bookmark keyword with parens, (BookmarkName)</param>
        /// <returns></returns>
        public Result<int> FindBookmark(List<string> script, string bookmark)
        {
            for (var i = 0; i < script.Count; i++)
            {
                if (script[i] == bookmark)
                    return Result.Ok(i);
            }
            return Result.Fail<int>("Bookmark " + bookmark + " is not in this script.");
        }

        private IScriptAccessor _getScripts;
        private LineService _lineService;
    }
}
