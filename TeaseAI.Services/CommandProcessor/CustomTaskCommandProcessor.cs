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

        public CustomTaskCommandProcessor(LineService lineService, IConfigurationAccessor configurationAccessor, IRandomNumberService randomNumberService,
            IVariableAccessor variableAccessor)
        {
            _lineService = lineService;
            _configurationAccessor = configurationAccessor;
            _randomNumberService = randomNumberService;
            _variableAccessor = variableAccessor;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.CustomTask);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.CustomTask);

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

            var filePath = _configurationAccessor.GetBaseFolder() + "\\Scripts\\" + workingSession.Domme.PersonalityName + "\\Custom\\Tasks\\" + fileName;
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
    }
}
