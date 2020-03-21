using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// The @DeleteFlag() Command deletes specified Flags in System\Flags and System\Flags\Temp.
    /// You can use multiple @DeleteFlag() Commands in the same line to delete multiple flags at once (For example, @DeleteFlag(Flag1) @DeleteFlag(Flag2)).
    /// You can also delete multiple flags at once by separating them in single @DeleteFlag() Commands with a comma (For example, @DeleteFlag(Flag1, Flag2, Flag3)).
    public class DeleteFlagCommandProcessor : CommandProcessorBase
    {
        public DeleteFlagCommandProcessor(IFlagAccessor flagService, LineService lineService) : base(Keyword.DeleteFlag, lineService)
        {
            _flagService = flagService;
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            _lineService.GetParenData(line, Keyword.DeleteFlag)
                .OnSuccess(pd =>
                {
                    foreach (var flagName in pd)
                    {
                        _flagService.DeleteFlag(session.Domme, flagName);
                    }
                });

            OnCommandProcessed(session);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => _lineService.GetParenData(line, Keyword.DeleteFlag).Map();

        private readonly IFlagAccessor _flagService;
        private readonly LineService _lineService;
    }
}
