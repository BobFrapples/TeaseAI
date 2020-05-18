using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ChastityOff : CommandProcessorBase
    {
        public ChastityOff(LineService lineService, ISettingsAccessor settingsAccessor) : base(Keyword.ChastityOff, lineService)
        {
            _settingsAccessor = settingsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (session.Domme.IsAfk)
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Sub.InChastity = false;
            var settings = _settingsAccessor.GetSettings();
            settings.Misc.IsInChastity = false;
            _settingsAccessor.WriteSettings(settings);

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private readonly ISettingsAccessor _settingsAccessor;
    }
}
