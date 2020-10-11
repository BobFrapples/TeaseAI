using System;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public abstract class CommandProcessorBase : ICommandProcessor
    {
        protected readonly string _keyword;
        protected LineService _lineService;

        public CommandProcessorBase(string keyword, LineService lineService)
        {
            _keyword = keyword;
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> BeforeCommandProcessed;
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public abstract Result<Session> PerformCommand(Session session, string line);

        /// <summary>
        /// <para>Validate the syntax and script requirements for this command.</para>
        /// <para>Variables should be ignored here</para>
        /// </summary>
        /// <param name="script"></param>
        /// <param name="personalityName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        protected abstract Result ParseCommandSpecific(Script script, string personalityName, string line);

        /// <summary>
        /// <para>Validate the syntax and script requirements for this command.</para>
        /// <para>Variables and interpolation are ignored for parsing unless explicitly relevant to the command</para>
        /// </summary>
        /// <param name="script"></param>
        /// <param name="personalityName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public virtual Result ParseCommand(Script script, string personalityName, string line)
        {
            return Result.Ok()
                .Ensure(() => IsRelevant(line), _keyword + " is not used in this line")
                .OnSuccess(() => ParseCommandSpecific(script, personalityName, line));
        }

        public virtual string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, _keyword);

        public virtual bool IsRelevant(string line) => line.Contains(_keyword);

        public virtual bool IsRelevant(Session session, string line) => IsRelevant(line);

        /// <summary>
        /// Fires <see cref="BeforeCommandProcessed"/> event passing session to subscribers.
        /// </summary>
        /// <param name="session"></param>
        protected virtual void OnBeforeCommandProcessed(Session session) => BeforeCommandProcessed(session, null);

        /// <summary>
        /// Fires <see cref="BeforeCommandProcessed"/> event, passing <paramref name="session"/> and <paramref name="parameter"/> to subscribers
        /// </summary>
        /// <param name="session"></param>
        /// <param name="parameter"></param>
        protected virtual void OnBeforeCommandProcessed(Session session, object parameter)
        {
            BeforeCommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = parameter });
        }

        /// <summary>
        /// Fires <see cref="CommandProcessed"/> event passing session to subscribers.
        /// </summary>
        /// <param name="session"></param>
        protected virtual void OnCommandProcessed(Session session) => OnCommandProcessed(session, null);

        /// <summary>
        /// Fires <see cref="CommandProcessed"/> event, passing <paramref name="session"/> and <paramref name="parameter"/> to subscribers
        /// </summary>
        /// <param name="session"></param>
        /// <param name="parameter"></param>
        protected virtual void OnCommandProcessed(Session session, object parameter)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = parameter });
        }
    }
}
