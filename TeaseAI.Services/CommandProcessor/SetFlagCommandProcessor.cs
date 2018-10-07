using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    ///  The @SetFlag() Command creates a Flag in System\Flags. You can use multiple @SetFlag() Commands in the same line to set multiple flags at once (For example, @SetFlag(Flag1) @SetFlag(Flag2)).
    /// You can also set multiple flags at once by separating them in single @SetFlag() Commands with a comma (For example, @SetFlag(Flag1, Flag2, Flag3)).
    /// </summary>
    public class SetFlagCommandProcessor : ICommandProcessor
    {
        public SetFlagCommandProcessor(FlagService flagService, LineService lineService)
        {
            _flagService = flagService;
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.SetFlag);

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.SetFlag);

        public Result<Session> PerformCommand(Session session, string line)
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

        void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        private FlagService _flagService;
        private readonly LineService _lineService;
    }
}
