using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services
{
    public class VitalSubService : IVitalSubService
    {
        /// <summary>
        /// Exercises assigned to the sub.
        /// </summary>
        public const string ExerciseFile = "exerciselist.cld";

        /// <summary>
        /// Food the domme knows about. 
        /// </summary>
        public const string AvailableFood = "calorielist.txt";

        /// <summary>
        /// Food the sub has eaten
        /// </summary>
        public const string EatenFood = "calorieitems.txt";

        public VitalSubService(IPathsAccessor pathsAccessor
            , ILineCollectionFilter lineCollectionFilter
            , IRandomNumberService randomNumberService
            , ISettingsAccessor settingsAccessor
            , IScriptAccessor scriptAccessor
            )
        {
            _pathsAccessor = pathsAccessor;
            _lineCollectionFilter = lineCollectionFilter;
            _randomNumberService = randomNumberService;
            _settingsAccessor = settingsAccessor;
            _scriptAccessor = scriptAccessor;
        }

        public ExerciseAssignment GetExerciseFromDomme(Session session, string file)
        {
            var basePath = _pathsAccessor.GetPersonalityFolder(session.Domme.PersonalityName);
            var filteredLines = _lineCollectionFilter.FilterLines(session, basePath + file);
            var selectedLine = _randomNumberService.Roll(0, filteredLines.Count);
            return new ExerciseAssignment
            {
                Description = filteredLines[selectedLine],
                IsComplete = false,
            };
        }

        public List<ExerciseAssignment> GetAssignedExercises()
        {
            var exerciseAssignments = new List<ExerciseAssignment>();
            using (var fileStream = new FileStream(_pathsAccessor.GetVitalSubDir() + ExerciseFile, FileMode.OpenOrCreate))
            using (var reader = new BinaryReader(fileStream))
            {
                while (fileStream.Position < fileStream.Length)
                {
                    exerciseAssignments.Add(new ExerciseAssignment
                    {
                        Description = reader.ReadString(),
                        IsComplete = reader.ReadBoolean(),
                    });
                }
            }
            return exerciseAssignments;
        }

        public void SaveAssignedExercises(List<ExerciseAssignment> exerciseAssignments)
        {
            using (var fileStream = new FileStream(_pathsAccessor.GetVitalSubDir() + ExerciseFile, FileMode.OpenOrCreate))
            using (var writer = new BinaryWriter(fileStream))
            {
                foreach (var ea in exerciseAssignments)
                {
                    writer.Write(ea.Description);
                    writer.Write(ea.IsComplete);
                }
            }
        }

        public List<string> GetKnownFoodItems()
        {
            var dataFile = _pathsAccessor.GetVitalSubDir() + AvailableFood;
            return File.Exists(dataFile)
                ? File.ReadAllLines(dataFile).OrderBy(l => l).ToList()
                : new List<string>();
        }

        public void SaveKnownFoodItems(IEnumerable<string> foodknown)
        {
            var dataFile = _pathsAccessor.GetVitalSubDir() + AvailableFood;
            File.WriteAllLines(dataFile, foodknown);
        }

        public List<string> GetEatenFood()
        {
            var dataFile = _pathsAccessor.GetVitalSubDir() + EatenFood;
            return File.Exists(dataFile)
                ? File.ReadAllLines(dataFile).OrderBy(l => l).ToList()
                : new List<string>();
        }

        public void SaveEatenFood(IEnumerable<string> foodknown)
        {
            var dataFile = _pathsAccessor.GetVitalSubDir() + EatenFood;
            File.WriteAllLines(dataFile, foodknown);
        }

        public Result<Script> SubmitData(DommePersonality domme)
        {
            var healthGoals = _settingsAccessor.GetSettings().Sub.HealthGoals;
            var didMissExercises = GetAssignedExercises().Any(ae => !ae.IsComplete);
            var didOverEat = (healthGoals.CaloriesGoal - healthGoals.CaloriesConsumed) < 0;
            var didSubFail = didMissExercises || didOverEat;
            return GetVitalSubResponses(domme, didSubFail)
                .OnSuccess(sl => sl[_randomNumberService.Roll(0, sl.Count())])
                .OnSuccess(scr => new ScriptMetaData()
                {
                    Info = "VitalSub Submit Response",
                    IsBeg = false,
                    IsChastity = false,
                    IsEdge = false,
                    IsEnabled = true,
                    IsRestricted = false,
                    Key = scr,
                    Name = "VitalSub Submit Response",
                    SessionPhase = SessionPhase.End,
                })
                .OnSuccess(smd => _scriptAccessor.GetScript(smd));
        }

        public Result<List<string>> GetVitalSubResponses(DommePersonality domme, bool didVitalSubFail)
        {
            var vitalList = new List<string>();
            var vitalSubState = didVitalSubFail ? "Punishments" : "Rewards";

            var rewardsFolder = _pathsAccessor.GetVitalSubDir(domme.PersonalityName) + vitalSubState;
            foreach (var foundFile in Directory.GetFiles(rewardsFolder, "*.txt"))
            {
                vitalList.Add(foundFile);
            }
            if (vitalList.Any())
                return Result.Ok(vitalList);
            return Result.Fail<List<string>>("No " + vitalSubState + " were found! Please make sure you have files in the VitaSub directory for " + domme.PersonalityName + "!");
        }

        private readonly IPathsAccessor _pathsAccessor;
        private readonly ILineCollectionFilter _lineCollectionFilter;
        private readonly IRandomNumberService _randomNumberService;
        private readonly ISettingsAccessor _settingsAccessor;
        private readonly IScriptAccessor _scriptAccessor;
    }
}
