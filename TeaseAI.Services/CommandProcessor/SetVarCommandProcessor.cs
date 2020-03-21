using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class SetVarCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;
        private readonly IVariableAccessor _variableAccessor;

        public SetVarCommandProcessor(LineService lineService
            , IVariableAccessor variableAccessor) : base(Keyword.SetVar, lineService)
        {
            _lineService = lineService;
            _variableAccessor = variableAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var getPair = GetVariablePair(line)
                .OnSuccess(vars =>
                {
                    var variableName = vars.Item1.Substring(Keyword.SetVar.Length, (vars.Item1.Length - Keyword.SetVar.Length - 1)).Trim();
                    var variableValue = vars.Item2.Substring(1, (vars.Item2.Length - 2)).Trim();
#warning TODO: accomodate #random
                    if (variableValue[0] == '#')
                        variableValue = "0";
                    _variableAccessor.SetVariable(session.Domme, variableName, variableValue);


                    OnCommandProcessed(session);

                    return Result.Ok(session);
                });
            return getPair;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return GetVariablePair(line)
                .Map();
        }

        private Result<Tuple<string, string>> GetVariablePair(string line)
        {
            if (!line.Contains("="))
                return Result.Fail<Tuple<string, string>>(line + " does not set a variable");
            var variableCommandStart = line.IndexOf(Keyword.SetVar);
            var equalsSign = line.Substring(variableCommandStart).IndexOf('=') + variableCommandStart;
            var variableCommandEnd = line.Substring(equalsSign).IndexOf(']') + equalsSign;

            var tokens = line.Substring(variableCommandStart, variableCommandEnd - variableCommandStart).Split('=').ToList();
            if (tokens.Count() > 2)
                return Result.Fail<Tuple<string, string>>(line + " cannot have = in the variable name or value");

            if(tokens.Count() < 2)
                return Result.Fail<Tuple<string, string>>(line + " does not use a format of @SetVar[VARNAME] = [VALUE]");

            return Result.Ok(Tuple.Create(tokens[0], tokens[1]));
        }
    }
}
