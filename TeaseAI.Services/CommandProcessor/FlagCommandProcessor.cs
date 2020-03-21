using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class FlagCommandProcessor : CommandProcessorBase
    {
        public FlagCommandProcessor(IFlagAccessor flagAccessor, LineService lineService) : base(Keyword.Flag, lineService)
        {
            _flagAccessor = flagAccessor;
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var flag = _lineService.GetParenData(line, Keyword.Flag);
            if (flag.IsFailure)
                return Result.Fail<Session>(flag.Error);

            if (_flagAccessor.IsSet(session.Domme, flag.Value[0]))
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.CurrentScript.LineNumber++;
            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => _lineService.GetParenData(line, Keyword.Flag).Map();

        private readonly IFlagAccessor _flagAccessor;
        private readonly LineService _lineService;
    }
}
