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
    public class GotoDommeRuinCommandProcessor : CommandProcessorBase
    {
        public GotoDommeRuinCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService) : base(Keyword.GotoDommeRuin, lineService)
        {
            _bookmarkService = bookmarkService;
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var result = _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines.ToList(), "(" + workingSession.Domme.RuinsOrgasms.ToString() + " Ruins)")
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var errors = new List<string>();
            foreach (var ruinsOrgasms in new List<string> { "Never", "Rarely", "Sometimes", "Often", "Always" })
            {
                var findBookmark = _bookmarkService.FindBookmark(script.Lines, "(" + ruinsOrgasms + ")");
                if (findBookmark.IsFailure)
                    errors.Add(findBookmark.Error.Message);
            }

            if (errors.Count == 0)
                return Result.Ok();

            return Result.Fail(string.Join(Environment.NewLine, errors));
        }


        private readonly IBookmarkService _bookmarkService;
        private LineService _lineService;
    }
}
