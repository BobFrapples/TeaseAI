using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    class CustomTaskCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;
        private readonly IConfigurationAccessor _configurationAccessor;
        private readonly IRandomNumberService _randomNumberService;
        private readonly IVariableAccessor _variableAccessor;
        private readonly IPathsAccessor _pathsAccessor;

        public CustomTaskCommandProcessor(LineService lineService
            , IConfigurationAccessor configurationAccessor
            , IRandomNumberService randomNumberService
            , IVariableAccessor variableAccessor
            , IPathsAccessor pathsAccessor) : base(Keyword.CustomTask, lineService)
        {
            _lineService = lineService;
            _configurationAccessor = configurationAccessor;
            _randomNumberService = randomNumberService;
            _variableAccessor = variableAccessor;
            _pathsAccessor = pathsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (!session.Sub.Kinks.Contains(Kink.CockTorture))
                return Result.Ok(session);

            var workingSession = session.Clone();
            var getTaskData = _lineService.GetParenData(line, Keyword.CustomTask);
            if (getTaskData.IsFailure)
                return Result.Fail<Session>(getTaskData.Error);

            var fileName = getTaskData.Value[0] + (workingSession.Sub.BallsTortureCount == 0 ? "_First" : string.Empty) + ".txt";
            var waitTime = _randomNumberService.Roll(workingSession.MinimumTaskTime, workingSession.MaximumTaskTime);
            if (getTaskData.Value.Count > 1)
                int.TryParse(getTaskData.Value[1], out waitTime);

            var filePath = _pathsAccessor.GetPersonalityFolder(workingSession.Domme.PersonalityName)
                + Path.DirectorySeparatorChar + "Custom"
                + Path.DirectorySeparatorChar + "Tasks"
                + Path.DirectorySeparatorChar + fileName;

            var lines = File.ReadAllLines(filePath);
            var message = lines[_randomNumberService.Roll(0, lines.Count())];
            var smd = new Script(new ScriptMetaData
            {
                Info = "Custom Task Interrupt",
                IsEnabled = true,
                Key = "CBT",
                Name = "Ball Torture" + workingSession.Sub.BallsTortureCount.ToString()
            },
             new List<string>
             {
                 message,
                 Keyword.Wait + waitTime.ToString() +")",
                 Keyword.End,
             });
            workingSession.Scripts.Push(smd);

            OnCommandProcessed(workingSession, null);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var getTaskData = _lineService.GetParenData(line, Keyword.CustomTask)
                .Ensure(pd => pd.Count == 1, Keyword.CustomTask + " can only have one parameter")
                .OnSuccess(pd =>
            {
                var errors = new List<string>();
                var fileName = GetFullFilename(personalityName, pd[0] + ".txt");
                if (!File.Exists(fileName))
                    errors.Add(fileName + " is missing, please create it.");
                var firstFileName = GetFullFilename(personalityName, pd[0] + "_First.txt");
                if (!File.Exists(firstFileName))
                    errors.Add(firstFileName + " is missing, please create it.");

                if (errors.Count == 0)
                    return Result.Ok();

                return Result.Fail(string.Join(Environment.NewLine, errors));
            });
            return getTaskData;
        }

        private string GetFullFilename(string personalityName, string fileName)
        {
            return _pathsAccessor.GetPersonalityFolder(personalityName)
                        + Path.DirectorySeparatorChar + "Custom"
                        + Path.DirectorySeparatorChar + "Tasks"
                        + Path.DirectorySeparatorChar + fileName;
        }
    }
}
