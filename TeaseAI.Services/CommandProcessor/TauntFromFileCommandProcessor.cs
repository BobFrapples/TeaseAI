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
    public class TauntFromFileCommandProcessor : CommandProcessorBase
    {
        private readonly IPathsAccessor _pathsAccessor;
        private readonly ILineCollectionFilter _lineCollectionFilter;
        private readonly IRandomNumberService _randomNumberService;

        public TauntFromFileCommandProcessor(LineService lineService
            , IPathsAccessor pathsAccessor
            , ILineCollectionFilter lineCollectionFilter
            , IRandomNumberService randomNumberService) : base(Keyword.TauntFromFile, lineService)
        {
            _pathsAccessor = pathsAccessor;
            _lineCollectionFilter = lineCollectionFilter;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var personalityFolder = _pathsAccessor.GetPersonalityFolder(session.Domme.PersonalityName);
            return _lineService.GetParenData(line, _keyword)
                .Ensure(pData => pData.Count == 1, "@TauntFromFile(...) must have only one option")
                .OnSuccess(pData => pData[0])
                .OnSuccess(fileArg => personalityFolder + Path.DirectorySeparatorChar + fileArg)
                .Ensure(file => File.Exists(file), "Unable to find the file specified in " + personalityFolder)
                .OnSuccess(file => _lineCollectionFilter.FilterLines(session, file))
                .OnSuccess(lines => lines[_randomNumberService.Roll(0, lines.Count() - 1)])
                .OnSuccess(l => OnCommandProcessed(session, l))
                .Map(l => session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var personalityFolder = _pathsAccessor.GetPersonalityFolder(personalityName);
            return _lineService.GetParenData(line, _keyword)
               .Ensure(pData => pData.Count == 1, "@TauntFromFile(...) must have only one option")
               .OnSuccess(pData => pData[0])
               .OnSuccess(fileArg => personalityFolder + Path.DirectorySeparatorChar + fileArg)
               .Ensure(file => File.Exists(file), "Unable to find the file specified in " + personalityFolder)
               .Map()
               ;
        }
    }
}
