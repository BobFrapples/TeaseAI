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
    ///<summary>
    /// The @Chance Command gives a chance to either jump to the line specified, or move to the next line as normal. The odds of jumping to the specified line are indicated in the Command
    /// itself. For example, @Chance50(Domme Instructions) would have a 50% chance of jumping to (Domme Instructions).
    ///</summary>
    public class ChanceCommandProcessor : CommandProcessorBase
    {
        public ChanceCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService) : base(Keyword.Chance, lineService)
        {
            _lineService = lineService;
            _bookmarkService = bookmarkService;
        }

        public override string DeleteCommandFrom(string input)
        {
            if (!input.Contains(Keyword.Chance))
                return input;
            var chance = input.Substring(input.IndexOf(Keyword.Chance) + Keyword.Chance.Length, 2);

            return _lineService.DeleteCommand(input, Keyword.Chance + chance + "(");
        }

        public override Result<Session> PerformCommand(Session session, string line)
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
                    .OnSuccess(bm => _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines.ToList(), bm))
                    .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                    .Map(ln => workingSession)
                    .OnSuccess(sesh => OnCommandProcessed(sesh));
                return result;
            }
            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var chance = line.Substring(line.IndexOf(Keyword.Chance) + Keyword.Chance.Length, 2);
            var chanceNum = 0;
            if (!int.TryParse(chance, out chanceNum))
                return Result.Fail("Unable to determine a chance percentage for " + chance);
            var result = _lineService.GetParenData(line, Keyword.Chance + chance + "(")
                .OnSuccess(pData =>
                {
                    var results = new List<Result>();
                    foreach (var bookmark in pData)
                    {
                        results.Add(_bookmarkService.FindBookmark(script.Lines, "(" + bookmark + ")").Map());
                    }

                    if (results.All(r => r.IsSuccess))
                        return Result.Ok();
                    return Result.Fail(string.Join(Environment.NewLine, results
                        .Where(r => r.IsFailure)
                        .Select(r => r.Error.Message)));
                });
            return result;
        }

        private readonly LineService _lineService;
        private readonly IBookmarkService _bookmarkService;
    }
}
