using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services
{
    /// <summary>
    /// Service to process / handle @Checkflag and @SetFlag
    /// </summary>
    public class FlagService
    {
        public FlagService(IFlagAccessor flagAccessor)
        {
            _flagAccessor = flagAccessor;
            _lineService = new LineService();
        }

        public string DeleteGetCommand(string input) => _lineService.DeleteCommand(input, Keyword.CheckFlag);
        public string DeleteSetCommand(string input) => _lineService.DeleteCommand(input, Keyword.SetFlag);
        public string DeleteSetTempCommand(string input) => _lineService.DeleteCommand(input, Keyword.SetTempFlag);

        /// <summary>
        /// Returns (BookmarkName) if the line contains a matching flag, string.Empty if not
        /// The @CheckFlag() Command checks to see if a Flag has previously been created by @SetFlag or @TempFlag, and goes to the appropriate line if it has. If you use @CheckFlag() with just the name of
        /// the flag itself, such as @CheckFlag(FlagName) , then Tease AI will move to the line (FlagName) if that Flag exists. However, you can also specify a line to go to if that Flag is found by using
        /// a comma, such as @CheckFlag(FlagName, Domme Instructions) . In this case, Tease AI would move to the line (Domme Instructions) if the Flag "FlagName" exists. You can use as many @CheckFlag()
        /// Commands per line that you wish. When specifiying a line to go to in a @CheckFlag Command, never put it in its own parentheses (For example, @CheckFlag(FlagName, Domme Instructions) is correct,
        /// @CheckFlag(FlagName, (Domme Instructions)) is incorrect. 
        /// </summary>
        /// <param name="input">input line</param>
        /// <returns></returns>
        public string GetNextStep(DommePersonality domme, string input)
        {
            var subString = input;
            while (subString.Contains(Keyword.CheckFlag))
            {
                var checkFlag = _lineService.GetParenData(input, Keyword.CheckFlag)
                    .OnSuccess(pd =>
                    {
                        var isSet = _flagAccessor.IsSet(domme, pd[0]);
                        if (isSet)
                            return Result.Ok((pd.Count == 1) ? pd[0] : pd[1]);
                        return Result.Fail<string>("Not Found");
                    });
                if (checkFlag.IsSuccess)
                    return "(" + checkFlag.Value +")";
                subString = input.Substring(input.IndexOf(Keyword.CheckFlag ) + Keyword.CheckFlag.Length);
            }

            return string.Empty;
        }

        /// <summary>
        /// The @SetFlag() Command creates a Flag in System\Flags. You can use multiple @SetFlag() Commands in the same line to set multiple flags at once (For example, @SetFlag(Flag1) @SetFlag(Flag2)).
        /// You can also set multiple flags at once by separating them in single @SetFlag() Commands with a comma (For example, @SetFlag(Flag1, Flag2, Flag3)).
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="input"></param>
        public void SetFlags(DommePersonality domme, string input)
        {
            var subString = input;
            while (subString.Contains(Keyword.SetFlag))
            {
                var setFlag = _lineService.GetParenData(input, Keyword.SetFlag)
                    .OnSuccess(flags =>
                    {
                        foreach(var flag in flags)
                        {
                            _flagAccessor.SetFlag(domme, flag, false);
                        }
                    });
                subString = input.Substring(input.IndexOf(Keyword.SetFlag) + Keyword.SetFlag.Length);
            }
        }

        /// <summary>
        /// The @TempFlag() Command creates a Flag in System\Flags\Temp. These work like @SetFlag() Commands, the only difference is that Flags set this way are deleted the next time Tease AI is run.
        /// You can use multiple @TempFlag() Commands in the same line to set multiple flags at once (For example, @TempFlag(Flag1) @TempFlag(Flag2)).
        /// You can also set multiple flags at once by separating them in single @TempFlag() Commands with a comma (For example, @TempFlag(Flag1, Flag2, Flag3)).
        /// </summary>
        /// <param name="domme"></param>
        /// <param name="input"></param>
        public void SetTempFlags(DommePersonality domme, string input)
        {
            var subString = input;
            while (subString.Contains(Keyword.SetTempFlag))
            {
                var setFlag = _lineService.GetParenData(input, Keyword.SetTempFlag)
                    .OnSuccess(flags =>
                    {
                        foreach (var flag in flags)
                        {
                            _flagAccessor.SetFlag(domme, flag, true);
                        }
                    });
                subString = input.Substring(input.IndexOf(Keyword.SetTempFlag) + Keyword.SetTempFlag.Length);
            }
        }

        private readonly IFlagAccessor _flagAccessor;
        private readonly LineService _lineService;
    }
}
