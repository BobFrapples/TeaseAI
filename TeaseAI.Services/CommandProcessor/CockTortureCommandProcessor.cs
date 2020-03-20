using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class CockTortureCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;
        private readonly IConfigurationAccessor _configurationAccessor;
        private readonly IRandomNumberService _randomNumberService;
        private readonly IPathsAccessor _pathsAccessor;

        public CockTortureCommandProcessor(LineService lineService
            , IConfigurationAccessor configurationAccessor
            , IRandomNumberService randomNumberService
            , IPathsAccessor pathsAccessor) : base(Keyword.CockTorture, lineService)

        {
            _lineService = lineService;
            _configurationAccessor = configurationAccessor;
            _randomNumberService = randomNumberService;
            _pathsAccessor = pathsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (!session.Sub.Kinks.Contains(Kink.CockTorture))
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.Sub.IsCockBeingTortured = true;
            var file = _configurationAccessor.GetBaseFolder() + "\\Scripts\\" + workingSession.Domme.PersonalityName + "\\CBT\\CBTCock.txt";
            if (workingSession.Sub.CockTortureCount == 0)
                file = _configurationAccessor.GetBaseFolder() + "\\Scripts\\" + workingSession.Domme.PersonalityName + "\\CBT\\CBTCock_First.txt";

            var lines = System.IO.File.ReadAllLines(file);
            var message = lines[_randomNumberService.Roll(0, lines.Count())];
            var waitTime = _randomNumberService.Roll(workingSession.MinimumTaskTime, workingSession.MaximumTaskTime);
            var smd = new Script(new ScriptMetaData
            {
                Info = "CBT Interrupt",
                IsEnabled = true,
                Key = "CBT",
                Name = "Cock Torture" + workingSession.Sub.CockTortureCount.ToString()
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
            var cbtFolder = _pathsAccessor.GetPersonalityFolder(personalityName) + Path.DirectorySeparatorChar + "cbt" + Path.DirectorySeparatorChar;
            return Result.Ok()
                .Ensure(() => File.Exists(cbtFolder + "cbtball.txt"), "Ball torture script does not exist at " + cbtFolder + "cbtball.txt")
                .Ensure(() => File.Exists(cbtFolder + "cbtball_first.txt"), "First ball torture script does not exist at " + cbtFolder + "cbtball_first.txt");
        }
    }
}
