using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class DecideOrgasmCommandProcessor : CommandProcessorBase
    {
        public const string OrgasmAllowBookmark = "(Orgasm Allow)";
        public const string OrgasmRuinBookmark = "(Orgasm Ruin)";
        public const string OrgasmDenyBookmark = "(Orgasm Deny)";

        public DecideOrgasmCommandProcessor(LineService lineService
            , IRandomNumberService randomNumberService
            , IBookmarkService bookmarkService
            , ISettingsAccessor settingsAccessor
            ) : base(Keyword.DecideOrgasm, lineService)
        {
            _randomNumberService = randomNumberService;
            _bookmarkService = bookmarkService;
            _settingsAccessor = settingsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.IsOrgasmAllowed = false;
            workingSession.IsOrgasmRuined = false;

            // TODO: Allow custom orgasm percentages
            var orgasmChance = _randomNumberService.RollPercent();
            workingSession.IsOrgasmAllowed = orgasmChance < workingSession.Domme.AllowsOrgasms;

            var ruinChance = _randomNumberService.RollPercent();
            workingSession.IsOrgasmRuined = workingSession.IsOrgasmAllowed && ruinChance < workingSession.Domme.RuinsOrgasms;

            var decisionBookmark = GetDecisionBookmark(workingSession.IsOrgasmAllowed, workingSession.IsOrgasmRuined);

            var result =  _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines, decisionBookmark)
                 .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                 .Map(ln => workingSession)
                 .OnSuccess(sesh => OnCommandProcessed(sesh));

            return result;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var results = new List<Result>();
            results.Add(_bookmarkService.FindBookmark(script.Lines, OrgasmAllowBookmark).Map());
            results.Add(_bookmarkService.FindBookmark(script.Lines, OrgasmRuinBookmark).Map());
            results.Add(_bookmarkService.FindBookmark(script.Lines, OrgasmDenyBookmark).Map());

            var errorMessage = string.Join(Environment.NewLine, results.Where(r => r.IsFailure).Select(r => r.Error.Message));
            if (string.IsNullOrWhiteSpace(errorMessage))
                return Result.Ok();

            return Result.Fail(errorMessage);
        }

        private string GetDecisionBookmark(bool isOrgasmAllowed, bool isOrgasmRuined)
        {
            if (isOrgasmAllowed && isOrgasmRuined)
                return OrgasmRuinBookmark;
            if (isOrgasmAllowed && !isOrgasmRuined)
                return OrgasmAllowBookmark;
            return OrgasmDenyBookmark;
        }

        private readonly IRandomNumberService _randomNumberService;
        private readonly IBookmarkService _bookmarkService;
        private readonly ISettingsAccessor _settingsAccessor;
    }
}
