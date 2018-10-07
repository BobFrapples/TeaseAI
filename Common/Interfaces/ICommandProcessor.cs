using System;
using TeaseAI.Common.Events;

namespace TeaseAI.Common.Interfaces
{
    public interface ICommandProcessor
    {
        event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        bool IsRelevant(Session session, string line);

        Result<Session> PerformCommand(Session session, string line);

        string DeleteCommandFrom(string line);
    }
}
