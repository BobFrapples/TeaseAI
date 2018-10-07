using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// The @DeleteFlag() Command deletes specified Flags in System\Flags and System\Flags\Temp.
    /// You can use multiple @DeleteFlag() Commands in the same line to delete multiple flags at once (For example, @DeleteFlag(Flag1) @DeleteFlag(Flag2)).
    /// You can also delete multiple flags at once by separating them in single @DeleteFlag() Commands with a comma (For example, @DeleteFlag(Flag1, Flag2, Flag3)).
    public class DeleteFlagCommandProcessor : ICommandProcessor
    {
        public DeleteFlagCommandProcessor(IFlagAccessor flagService, LineService lineService)
        {
            _flagService = flagService;
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.DeleteFlag);

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.DeleteFlag);

        public Result<Session> PerformCommand(Session session, string line)
        {
            _lineService.GetParenData(line, Keyword.SetFlag)
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

        void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        private readonly IFlagAccessor _flagService;
        private readonly LineService _lineService;
    }
}
