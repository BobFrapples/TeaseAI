using System;
using System.Collections.Generic;
using System.IO;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class RequestTaskCommandProcessor : CommandProcessorBase
    {
        public RequestTaskCommandProcessor(LineService lineService
            , ITimeService timeService
            , IPathsAccessor pathsAccessor
            , ILineCollectionFilter lineCollectionFilter
            , IRandomNumberService randomNumberService
            ) : base(Keyword.SendDailyTasks, lineService)
        {
            _timeService = timeService;
            _pathsAccessor = pathsAccessor;
            _lineCollectionFilter = lineCollectionFilter;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();

            var taskLetter = CreateTaskLetter(session);

            OnCommandProcessed(workingSession, taskLetter);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            if (!Directory.Exists(_pathsAccessor.GetPersonalityFolder(personalityName) + "/tasks/"))
                return Result.Fail("No tasks folder has been created for this personality");

            var missingFiles = new List<string>();
            foreach (var name in new List<string> { "greeting.txt" })
            {
                if (!File.Exists(_pathsAccessor.GetPersonalityFolder(personalityName) + "/tasks/" + name))
                    missingFiles.Add(name);
            }
            return Result.Fail("This personality needs these tasks files: " + string.Join(" ", missingFiles));
        }

        private TaskLetter CreateTaskLetter(Session session)
        {
            var generalTime = _timeService.GetGeneralTime();
            var taskLetter = new TaskLetter();
            taskLetter.Body = GetLine(session, "/tasks/greeting.txt") + Environment.NewLine + Environment.NewLine;
            taskLetter.Body += GetLine(session, "/tasks/intro.txt") + Environment.NewLine + Environment.NewLine;

            if (generalTime == "Morning")
            {
                taskLetter.Body += GetLine(session, "/tasks/task_1.txt") + Environment.NewLine + Environment.NewLine;
                taskLetter.Body += GetLine(session, "/tasks/link_1-2.txt") + Environment.NewLine + Environment.NewLine;
            }

            if (generalTime == "Morning" || generalTime == "Afternoon")
            {
                taskLetter.Body += GetLine(session, "/tasks/task_2.txt") + Environment.NewLine + Environment.NewLine;
                taskLetter.Body += GetLine(session, "/tasks/link_2-3.txt") + Environment.NewLine + Environment.NewLine;
            }
            taskLetter.Body += GetLine(session, "/tasks/task_3.txt") + Environment.NewLine + Environment.NewLine;
            taskLetter.Body += GetLine(session, "/tasks/outro.txt") + Environment.NewLine + Environment.NewLine;
            taskLetter.Body += GetLine(session, "/tasks/signature.txt") + Environment.NewLine + Environment.NewLine;

            if (session.Domme.RequiresHonorific)
                taskLetter.Body += session.Domme.Honorific + " " + session.Domme.Name;
            else
                taskLetter.Body += session.Domme.Name;


            return taskLetter;
        }

        private string GetLine(Session session, string file)
        {
            var basePath = _pathsAccessor.GetPersonalityFolder(session.Domme.PersonalityName);
            var lines = File.ReadAllLines(basePath + file);
            var filteredLines = _lineCollectionFilter.FilterLines(session, lines);
            var selectedLine = _randomNumberService.Roll(0, filteredLines.Count);
            return filteredLines[selectedLine];
        }

        private readonly ITimeService _timeService;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly ILineCollectionFilter _lineCollectionFilter;
        private readonly IRandomNumberService _randomNumberService;
    }
}
