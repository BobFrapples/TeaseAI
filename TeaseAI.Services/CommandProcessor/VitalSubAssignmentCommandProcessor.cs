using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class VitalSubAssignmentCommandProcessor : CommandProcessorBase
    {
        public VitalSubAssignmentCommandProcessor(LineService lineService
            , ISettingsAccessor settingsAccessor
            , ILineCollectionFilter lineCollectionFilter
            , IRandomNumberService randomNumberService
            , IPathsAccessor pathsAccessor
            , IVitalSubService vitalSubService
            )
            : base(Keyword.VitalSubAssignment, lineService)
        {
            _settingsAccessor = settingsAccessor;
            _lineCollectionFilter = lineCollectionFilter;
            _pathsAccessor = pathsAccessor;
            _randomNumberService = randomNumberService;
            _vitalSubService = vitalSubService;
        }

        public override bool IsRelevant(Session session, string line) => base.IsRelevant(session, line) && _settingsAccessor.GetSettings().Sub.HealthGoals.CanDommeAddAssignments;

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var newExercise = _vitalSubService.GetExerciseFromDomme(workingSession, _pathsAccessor.GetVitalSubDir() + "assignments.txt");
            var assignedExercises = _vitalSubService.GetAssignedExercises();
            assignedExercises.Add(newExercise);
            _vitalSubService.SaveAssignedExercises(assignedExercises);
            
            OnCommandProcessed(workingSession, newExercise);

            return Result.Ok(workingSession);
        }

        private string GetLine(Session session, string file)
        {
            var basePath = _pathsAccessor.GetPersonalityFolder(session.Domme.PersonalityName);
            var filteredLines = _lineCollectionFilter.FilterLines(session, basePath + file);
            var selectedLine = _randomNumberService.Roll(0, filteredLines.Count);
            return filteredLines[selectedLine];
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private readonly ISettingsAccessor _settingsAccessor;
        private readonly ILineCollectionFilter _lineCollectionFilter;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly IRandomNumberService _randomNumberService;
        private readonly IVitalSubService _vitalSubService;
    }
}
