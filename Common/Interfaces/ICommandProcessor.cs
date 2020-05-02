using System;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;

namespace TeaseAI.Common.Interfaces
{
    public interface ICommandProcessor
    {
         event EventHandler<CommandProcessedEventArgs> BeforeCommandProcessed;
        event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        Result<Session> PerformCommand(Session session, string line);

        string DeleteCommandFrom(string line);

        /// <summary>
        /// Confirm the command is correct within the context of the script / personality
        /// </summary>
        /// <param name="script"></param>
        /// <param name="personalityName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        Result ParseCommand(Script script, string personalityName, string line);

        /// <summary>
        /// Is this command relevant to this line? 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        bool IsRelevant(string line);

        [Obsolete("refactor to use the version without session")]
        /// <summary>
        /// Does the command show up in <paramref name="line"/>
        /// </summary>
        /// <param name="session"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        bool IsRelevant(Session session, string line);
    }
}
