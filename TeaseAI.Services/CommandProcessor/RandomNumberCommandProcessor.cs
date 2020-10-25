using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class RandomNumberCommandProcessor : CommandProcessorBase
    {
        public RandomNumberCommandProcessor(LineService lineService
            , IRandomNumberService randomNumberService) : base(Keyword.RandomNumber, lineService)
        {
            _randomNumberService = randomNumberService;
        }

        public override string DeleteCommandFrom(string line)
        {
            if (!IsRelevant(line))
                return line;
            var getNumber = GetCommandOptions(line)
                .OnSuccess(p => Tuple.Create(int.Parse(p.Item1), int.Parse(p.Item2)))
                .OnSuccess(p => _randomNumberService.Roll(Math.Min(p.Item1, p.Item2), Math.Max(p.Item1, p.Item2)))
                .OnSuccess(n => _lineService.ReplaceParenData(line, _keyword, n.ToString()));
            return getNumber.GetResultOrDefault(getNumber.GetErrorMessageOrDefault());
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var getOptions = GetCommandOptions(line);
            if (getOptions.IsFailure)
                return getOptions.Map(o => session);

            return getOptions
                .Ensure(p => int.TryParse(p.Item1, out int minimum), getOptions.Value.Item1 + " is text, minimum must be a number.")
                .Ensure(p => int.TryParse(p.Item2, out int maximum), getOptions.Value.Item2 + " is text, maximum must be a number.")
                .Map(p => session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var parse = GetCommandOptions(line)
                .Ensure(p => int.TryParse(p.Item1, out int minimum) || (p.Item1.StartsWith("{") && p.Item1.EndsWith("}")), "Minimum must be either a number or interpolation.")
                .Ensure(p => int.TryParse(p.Item2, out int minimum) || (p.Item2.StartsWith("{") && p.Item2.EndsWith("}")), "Maximum must be either a number or interpolation.")
                .Map();

            return parse;
        }

        private Result<Tuple<string, string>> GetCommandOptions(string line)
        {
            return _lineService.GetParenData(line, _keyword)
                 .Ensure(lines => lines.Count == 1 || lines.Count == 2, "@RandomNumber accepts only one or two parameters")
                 .OnSuccess(lines => lines.Count == 1 ? Tuple.Create("0", lines[0]) : Tuple.Create(lines[0], lines[1]));
        }

        private readonly IRandomNumberService _randomNumberService;
    }
}
