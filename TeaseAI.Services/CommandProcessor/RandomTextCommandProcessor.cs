using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;


namespace TeaseAI.Services.CommandProcessor
{
    public class RandomTextCommandProcessor : CommandProcessorBase
    {
        public RandomTextCommandProcessor(LineService lineService
            ,IRandomNumberService randomNumberService):base(Keyword.RandomText, lineService)
        {
            _randomNumberService = randomNumberService;
        }

        public override string DeleteCommandFrom(string input)
        {
            if (!IsRelevant(input))
                return input;

            var result = _lineService.GetParenData(input, _keyword)
                .OnSuccess(pd => pd[_randomNumberService.Roll(0, pd.Count)])
                .OnSuccess(text => _lineService.ReplaceParenData(input, _keyword, text));

            return result.Value;
        }

        /// <summary>
        /// This processor actually does its work in the DeleteCommandFrom method
        /// </summary>
        /// <param name="session"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public override Result<Session> PerformCommand(Session session, string line) => Result.Ok(session);

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, _keyword)
                .Ensure(pd => pd.Count > 0, _keyword + " requires at least one parameter")
                .Map();
        }

        private readonly IRandomNumberService _randomNumberService;
    }
}
