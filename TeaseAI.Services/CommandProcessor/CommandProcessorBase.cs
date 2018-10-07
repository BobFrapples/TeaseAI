using System;
using TeaseAI.Common;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public abstract class CommandProcessorBase : ICommandProcessor
    {
        public CommandProcessorBase()
        {
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public abstract string DeleteCommandFrom(string line);

        public abstract bool IsRelevant(Session session, string line);

        public abstract Result<Session> PerformCommand(Session session, string line);
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
