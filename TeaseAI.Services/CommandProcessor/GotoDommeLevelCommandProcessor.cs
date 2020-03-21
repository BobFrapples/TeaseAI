using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class GotoDommeLevelCommandProcessor : CommandProcessorBase
    {
        public GotoDommeLevelCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService) : base(Keyword.GotoDommeApathy, lineService)
        {
            _lineService = lineService;
            _bookmarkService = bookmarkService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var result = _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines.ToList(), GetDommeLevelBookmark(workingSession.Domme.DomLevel))
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var errors = new List<string>();
            foreach (var dommeLevel in new List<int> { 1, 2, 3, 4, 5 })
            {
                var findBookmark = _bookmarkService.FindBookmark(script.Lines, GetDommeLevelBookmark(dommeLevel));
                if (findBookmark.IsFailure)
                    errors.Add(findBookmark.Error.Message);
            }

            if (errors.Count == 0)
                return Result.Ok();

            return Result.Fail(string.Join(Environment.NewLine, errors));
        }

        private string GetDommeLevelBookmark(int dommeLevel) => "(DommeLevel" + dommeLevel.ToString() + ")";

        private readonly LineService _lineService;
        private readonly IBookmarkService _bookmarkService;
    }
}
