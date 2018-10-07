using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class CheckFlagCommandProcessor : ICommandProcessor
    {
        public CheckFlagCommandProcessor(IFlagAccessor flagAccessor, LineService lineService)
        {
            _flagAccessor = flagAccessor;
            _lineService = lineService;
        }
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.CheckFlag);

        public bool IsRelevant(Session session, string line)
        {
            return line.Contains(Keyword.CheckFlag);
        }

        /// <summary>
        /// Returns (BookmarkName) if the line contains a matching flag, string.Empty if not
        /// The @CheckFlag() Command checks to see if a Flag has previously been created by @SetFlag or @TempFlag, and goes to the appropriate line if it has. If you use @CheckFlag() with just the name of
        /// the flag itself, such as @CheckFlag(FlagName) , then Tease AI will move to the line (FlagName) if that Flag exists. You can use as many @CheckFlag()
        /// Commands per line that you wish. When specifiying a line to go to in a @CheckFlag Command, never put it in its own parentheses (For example, @CheckFlag(FlagName, Domme Instructions) is correct,
        /// @CheckFlag(FlagName, (Domme Instructions)) is incorrect. 
        /// </summary>
        /// <param name="line">input line</param>
        /// <returns></returns>
        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();

            var script = workingSession.CurrentScript;
            var target = GetNextStep(workingSession.Domme, line);
            if (string.IsNullOrWhiteSpace(target))
                script.LineNumber++;
            else if (!script.Lines.Contains(target))
                return Result.Fail<Session>("Bookmark " + target + " not found in the script, " + workingSession.CurrentScript.MetaData.Name);
            else
                script.LineNumber = script.Lines.IndexOf(target);

            return Result.Ok(workingSession);
        }

        private string GetNextStep(DommePersonality domme, string input)
        {
            var subString = input;
            while (subString.Contains(Keyword.CheckFlag))
            {
                var checkFlag = _lineService.GetParenData(input, Keyword.CheckFlag)
                    .OnSuccess(pd =>
                    {
                        foreach (var flag in pd)
                            if (_flagAccessor.IsSet(domme, flag))
                                return Result.Ok(flag);
                        return Result.Fail<string>("Not Found");
                    });
                if (checkFlag.IsSuccess)
                    return "(" + checkFlag.Value + ")";
                subString = input.Substring(input.IndexOf(Keyword.CheckFlag) + Keyword.CheckFlag.Length);
            }

            return string.Empty;
        }

        private readonly IFlagAccessor _flagAccessor;
        private readonly LineService _lineService;
    }
}
