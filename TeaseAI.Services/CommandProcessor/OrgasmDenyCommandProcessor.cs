using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;


namespace TeaseAI.Services.CommandProcessor
{
    public class OrgasmDenyCommandProcessor : CommandProcessorBase
    {
        public OrgasmDenyCommandProcessor(LineService lineService
             , IBookmarkService bookmarkService) : base(Keyword.OrgasmAllow, lineService)
        {
            _lineService = lineService;
            _bookmarkService = bookmarkService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.Sub.WillBeAllowedToOrgasm = false;

            var result = _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines, OrgasmDenyBookmark)
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var findBookmark = _bookmarkService.FindBookmark(script.Lines, OrgasmDenyBookmark);
            if (findBookmark.IsFailure)
                return Result.Fail(OrgasmDenyBookmark + " must be a bookmark in your script");

            return Result.Ok();
        }

        private string OrgasmDenyBookmark = "(Orgasm Deny)";
        private LineService _lineService;
        private IBookmarkService _bookmarkService;
    }
}
