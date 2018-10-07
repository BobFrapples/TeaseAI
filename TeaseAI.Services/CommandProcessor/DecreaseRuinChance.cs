using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class DecreaseRuinChanceCommand : ICommandProcessor
    {
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.DecreaseRuinChance, string.Empty);
        }

        public bool IsRelevant(Session session, string line)
        {
            return line.Contains(Keyword.DecreaseRuinChance);
        }

        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (line.Contains(Keyword.DecreaseRuinChance))
            {
                workingSession.Domme.RuinsOrgasms--;
            }
            CommandProcessed.Invoke(this, new CommandProcessedEventArgs() { Session = workingSession });

            return Result.Ok(workingSession);
        }
    }
}
