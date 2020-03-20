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
    public class CallCommandProcessor : CommandProcessorBase
    {
        public CallCommandProcessor(IScriptAccessor scriptAccessor
            , LineService lineService
            , IPathsAccessor pathsAccessor
            , IBookmarkService bookmarkService) : base(Keyword.Call, lineService)
        {
            _lineService = lineService;
            _scriptAccessor = scriptAccessor;
            _pathsAccessor = pathsAccessor;
            _bookmarkService = bookmarkService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var getOptions = _lineService.GetParenData(line, Keyword.Call);
            if (getOptions.IsFailure)
                return Result.Fail<Session>(getOptions.Error);
            var workingSession = session.Clone();
            Result<Script> newScript = _scriptAccessor.GetScript(workingSession.Domme, getOptions.Value[0]);
            if (newScript.IsFailure)
                return Result.Fail<Session>(newScript.Error);

            if (getOptions.Value.Count == 2)
            {
                var findBookmark = _bookmarkService.FindBookmark(newScript.Value.Lines, getOptions.Value[1]);
                if (findBookmark.IsFailure)
                    return Result.Fail<Session>(findBookmark.Error);
                newScript.Value.LineNumber = findBookmark.Value;
            }

            workingSession.Scripts.Push(newScript.Value);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityId, string line)
        {
            var getOptions = _lineService.GetParenData(line, Keyword.Call)
                .Ensure(pData => pData.Count == 2, Keyword.Call + " has an incorrect number of parameters in " + line)
                .OnSuccess(pData =>
                {
                    var fileName = _pathsAccessor.GetPersonalityFolder(personalityId) + Path.DirectorySeparatorChar + pData[0];
                    return _scriptAccessor.GetScript(fileName)
                        .OnSuccess(s => _bookmarkService.FindBookmark(s.Lines, pData[1]))
                        .Map();
                });

            return getOptions;
        }

        private LineService _lineService;
        private IScriptAccessor _scriptAccessor;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly IBookmarkService _bookmarkService;
    }
}
