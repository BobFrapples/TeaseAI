using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    ///  The @SetFlag() Command creates a Flag in System\Flags. You can use multiple @SetFlag() Commands in the same line to set multiple flags at once (For example, @SetFlag(Flag1) @SetFlag(Flag2)).
    /// You can also set multiple flags at once by separating them in single @SetFlag() Commands with a comma (For example, @SetFlag(Flag1, Flag2, Flag3)).
    /// </summary>
    public class SetFlagCommandProcessor : CommandProcessorBase
    {
        public SetFlagCommandProcessor(FlagService flagService
            , LineService lineService) : base(Keyword.SetFlag, lineService)
        {
            _flagService = flagService;
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            _lineService.GetParenData(line, Keyword.SetFlag)
                .OnSuccess(pd =>
                {
                    foreach (var flagName in pd)
                    {
                        _flagService.SetFlags(session.Domme, flagName);
                    }
                });

            OnCommandProcessed(session);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, Keyword.SetFlag)
                .Ensure(pd => pd.Any(), Keyword.SetFlag + " requires at least one parameter")
                .Map();
        }

        private readonly FlagService _flagService;
    }
}
