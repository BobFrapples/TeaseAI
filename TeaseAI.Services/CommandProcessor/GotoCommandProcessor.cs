using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Processor for the Goto command
    /// </summary>
    public class GotoCommandProcessor : CommandProcessorBase
    {
        public GotoCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService
            , IRandomNumberService randomNumberService) : base(Keyword.Goto, lineService)
        {
            _lineService = lineService;
            _bookmarkService = bookmarkService;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var result = _lineService.GetParenData(line, Keyword.Goto)
                 .OnSuccess(pd => "(" + pd[new Random().Next(0, pd.Count)] + ")")
                 .OnSuccess(bm => _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines, bm))
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, Keyword.Goto)
                .Ensure(pd => pd.Count > 0, Keyword.Goto + " must have at least one parameter")
                .OnSuccess(pd => "(" + pd[_randomNumberService.Roll(0, pd.Count)] + ")")
                .OnSuccess(bm => _bookmarkService.FindBookmark(script.Lines, bm))
                .Map();
        }

        private LineService _lineService;
        private readonly IBookmarkService _bookmarkService;
        private readonly IRandomNumberService _randomNumberService;
    }
}
