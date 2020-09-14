using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Allows the Domme to check on the sub's health goals at any time
    /// </summary>
    public class VitalSubSubmitDailyReportCommandProcessor : CommandProcessorBase
    {
        private readonly IVitalSubService _vitalSubService;

        public VitalSubSubmitDailyReportCommandProcessor(LineService lineService
            , IVitalSubService vitalSubService
            , ISettingsAccessor settingsAccessor
    )
    : base(Keyword.VitalSubSubmitDailyReport
          , lineService)
        {
            _vitalSubService = vitalSubService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            OnBeforeCommandProcessed(session);
            var workingSession = session.Clone();
            var response = _vitalSubService.SubmitData(session.Domme)
                .OnSuccess(s =>
                {
                    workingSession.Scripts.Push(s);
                    _vitalSubService.SaveAssignedExercises(new List<ExerciseAssignment>());
                    _vitalSubService.SaveEatenFood(new List<string>());

                    OnCommandProcessed(workingSession);
                    return workingSession;
                });
            return response;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();
    }
}
