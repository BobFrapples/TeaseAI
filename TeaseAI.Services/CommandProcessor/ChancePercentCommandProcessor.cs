using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    ///<summary>
    /// The @ChancePercent Command gives a chance to either jump to the line specified, or move to the next line as normal. The odds of jumping to the specified line are indicated in the Command
    /// itself. For example, @ChancePercent(50, Domme Instructions) would have a 50% chance of jumping to (Domme Instructions).
    ///</summary>
    public class ChancePercentCommandProcessor : CommandProcessorBase
    {
        public ChancePercentCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService
            , IRandomNumberService randomNumberService) : base(Keyword.ChancePercent, lineService)
        {
            _lineService = lineService;
            _bookmarkService = bookmarkService;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var result = GetCommandOptions(line)
                .OnSuccess(options =>
                {
                    var jumpRoll = _randomNumberService.RollPercent();
                    if (jumpRoll <= options.Item1)
                    {
                        var workingSession = session.Clone();
                        return _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines.ToList(), "(" + options.Item2 + ")")
                            .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                            .Map(ln => workingSession);
                    }
                    return Result.Ok(session);
                })
                .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var result = GetCommandOptions(line)
                .OnSuccess(options => _bookmarkService.FindBookmark(script.Lines, "(" + options.Item2 + ")"))
                .Map();

            return result;
        }

        private Result<Tuple<int, string>> GetCommandOptions(string line)
        {
            return _lineService.GetParenData(line, _keyword)
               .Ensure(p => p.Count == 2, _keyword + "...) must have exactly two parameters")
               .Ensure(p => int.TryParse(p[0], out var parsedInt) && 0 <= parsedInt && parsedInt <= 100, _keyword.Substring(0, _keyword.Length - 1) + "'s first parameter must be a number between 0 and 100")
               .OnSuccess(p => Tuple.Create(int.Parse(p[0]), p[1]));
        }

        private readonly IBookmarkService _bookmarkService;
        private readonly IRandomNumberService _randomNumberService;
    }
}
