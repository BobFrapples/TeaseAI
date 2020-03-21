using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    /// The @TempFlag() Command creates a Flag in System\Flags\Temp. These work like @SetFlag() Commands, the only difference is that Flags set this way are deleted the next time Tease AI is run.
    /// You can use multiple @TempFlag() Commands in the same line to set multiple flags at once (For example, @TempFlag(Flag1) @TempFlag(Flag2)).
    /// You can also set multiple flags at once by separating them in single @TempFlag() Commands with a comma (For example, @TempFlag(Flag1, Flag2, Flag3)).

    public class TempFlagCommandProcessor : CommandProcessorBase
    {
        public TempFlagCommandProcessor(FlagService flagService
            , LineService lineService) :base(Keyword.SetTempFlag,lineService)
        {
            _flagService = flagService;
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            _lineService.GetParenData(line, Keyword.SetTempFlag)
                .OnSuccess(pd =>
                {
                    foreach (var flagName in pd)
                    {
                        _flagService.SetTempFlags(session.Domme, flagName);
                    }
                });

            OnCommandProcessed(session);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, Keyword.SetTempFlag)
                .Ensure(pd => pd.Any(), Keyword.SetTempFlag + " requires at least one parameter")
                .Map();
        }

        private FlagService _flagService;
        private readonly LineService _lineService;
    }
}
