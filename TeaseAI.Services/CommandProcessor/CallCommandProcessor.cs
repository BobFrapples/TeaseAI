using System;
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

            var verify = GetTargets(line)
                .OnSuccess(targets =>
                {
                    var workingSession = session.Clone();
                    var newScript = _scriptAccessor.GetScript(workingSession.Domme, targets.Item1)
                        .OnSuccess(s =>
                         {
                             return _bookmarkService.FindBookmark(s.Lines, targets.Item2)
                                 .OnSuccess(ln => s.LineNumber = ln)
                                 .Map(ln => s);
                         })
                        .OnSuccess(s => workingSession.Scripts.Push(s));

                    return newScript.Map(s => workingSession);
                });
            return verify;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityId, string line)
        {
            var verify = GetTargets(line)
                .OnSuccess(targets =>
                {
                    var fileName = _pathsAccessor.GetPersonalityFolder(personalityId) + Path.DirectorySeparatorChar + targets.Item1;
                    return _scriptAccessor.GetScript(fileName)
                        .OnSuccess(s => _bookmarkService.FindBookmark(s.Lines, targets.Item2))
                        .Map();
                });
            return verify;
        }

        /// <summary>
        /// Get the target of the <seealso cref="Keyword.Call"/> command
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private Result<Tuple<string, string>> GetTargets(string line)
        {
            return _lineService.GetParenData(line, Keyword.Call)
                .Ensure(pData => pData.Count < 3, Keyword.Call + " may only have up to 2 parameters. " + line)
                .Ensure(pData => pData.Count > 0, Keyword.Call + " must have at least 1 parameter. " + line)
                .OnSuccess(pData =>
                {
                    var file = pData[0];
                    var bookmark = pData.Count == 2 ? pData[1] : "";
                    return Result.Ok(Tuple.Create(file, bookmark));
                });
        }

        private LineService _lineService;
        private IScriptAccessor _scriptAccessor;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly IBookmarkService _bookmarkService;
    }
}
