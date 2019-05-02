using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class SetVarCommandProcessor : CommandProcessorBase
    {
        private readonly LineService _lineService;
        private readonly IVariableAccessor _variableAccessor;

        public SetVarCommandProcessor(LineService lineService, IVariableAccessor variableAccessor)
        {
            _lineService = lineService;
            _variableAccessor = variableAccessor;
        }

        public override string DeleteCommandFrom(string line) => _lineService.DeleteCommand(line, Keyword.SetVar);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.SetVar);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            if (!line.Contains("="))
                return Result.Fail<Session>(line + " does not set a variable");
            var variableCommandStart = line.IndexOf(Keyword.SetVar);
            var equalsSign = line.Substring(variableCommandStart).IndexOf('=');
            var variableCommandEnd = line.Substring(equalsSign).IndexOf(']');

            var tokens = line.Substring(variableCommandStart, variableCommandStart - variableCommandEnd).Split('=').ToList();
            if (tokens.Count() > 2)
                return Result.Fail<Session>(line + " cannot have = in the variable name or value");

            var variableName = tokens[0].Substring(Keyword.SetVar.Length, (tokens[0].Length - Keyword.SetVar.Length - 1)).Trim();
            var variableValue = tokens[0].Substring(1, (tokens[0].Length - 2)).Trim();
#warning TODO: accomodate #random
            if (variableValue[0] == '#')
                variableValue = "0";
            _variableAccessor.SetVariable(session.Domme, variableName, variableValue);


            OnCommandProcessed(session);

            return Result.Ok(session);
        }
    }
}
