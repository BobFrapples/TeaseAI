using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class FlagCommandProcessor : CommandProcessorBase
    {
        public FlagCommandProcessor(IFlagAccessor flagAccessor, LineService lineService)
        {
            _flagAccessor = flagAccessor;
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.Flag);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.Flag);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var flag = _lineService.GetParenData(line, Keyword.NotFlag);
            if (flag.IsFailure)
                return Result.Fail<Session>(flag.Error);

            if (_flagAccessor.IsSet(session.Domme, flag.Value[0]))
                return Result.Ok(session);

            var workingSession = session.Clone();
            workingSession.CurrentScript.LineNumber++;
            return Result.Ok(workingSession);
        }

        private readonly IFlagAccessor _flagAccessor;
        private readonly LineService _lineService;
    }
}
