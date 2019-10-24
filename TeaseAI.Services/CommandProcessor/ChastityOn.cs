using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ChastityOn : CommandProcessorBase
    {
        public ChastityOn(LineService lineService, ISettingsAccessor settingsAccessor)
        {
            _lineService = lineService;
            _settingsAccessor = settingsAccessor;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.ChastityOn);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.ChastityOn);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (session.Domme.IsAfk)
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Sub.InChastity = true;
            _settingsAccessor.InChastity = true;

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        private readonly LineService _lineService;
        private readonly ISettingsAccessor _settingsAccessor;
    }
}
