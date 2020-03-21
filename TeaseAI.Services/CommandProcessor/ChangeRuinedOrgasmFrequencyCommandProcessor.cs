using System;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class ChangeRuinedOrgasmFrequencyCommandProcessor : ICommandProcessor
    {
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(@"@IncreaseRuinChance", string.Empty)
                .Replace(@"@DecreaseRuinChance", string.Empty);
        }

        public bool IsRelevant(Session session, string line) => IsRelevant(line);

        public bool IsRelevant(string line) => line.Contains(@"@IncreaseRuinChance") || line.Contains(@"@DecreaseRuinChance");

        public Result ParseCommand(Script script, string personalityName, string line)
        {
            throw new NotImplementedException();
        }

        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            if (line.Contains(@"@IncreaseRuinChance"))
                workingSession.Domme.RuinsOrgasms++;
            if (line.Contains(@"@DecreaseRuinChance"))
                workingSession.Domme.RuinsOrgasms++;
            return Result.Ok(workingSession);
        }
    }
}
