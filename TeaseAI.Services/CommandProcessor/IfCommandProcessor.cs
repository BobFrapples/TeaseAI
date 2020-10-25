using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class IfCommandProcessor : CommandProcessorBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IConditionalObjectLogic _conditionalObjectLogic;
        private readonly IVariableAccessor _variableAccessor;
        private readonly IVocabularyProcessor _sessionVocabularyProcessor;

        public IfCommandProcessor(LineService lineService
            , IBookmarkService bookmarkService
            , IConditionalObjectLogic conditionalObjectLogic
            , IVariableAccessor variableAccessor
            , IVocabularyProcessor sessionVocabularyProcessor
            ) : base(Keyword.If, lineService)
        {
            _bookmarkService = bookmarkService;
            _conditionalObjectLogic = conditionalObjectLogic;
            _variableAccessor = variableAccessor;
            _sessionVocabularyProcessor = sessionVocabularyProcessor;
        }

        public override string DeleteCommandFrom(string line)
        {
            var workingLine = line;
            var parseObjects = ParseObjects(line).GetResultOrDefault(new List<ConditionalObject>());

            parseObjects.ForEach(co => workingLine = workingLine.Replace(co.String, ""));
            return workingLine;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var parseObjects = ParseObjects(line);
            if (parseObjects.IsFailure)
                return Result.Fail<Session>(parseObjects.Error);

            parseObjects.Value.ForEach(co =>
            {
                if (_variableAccessor.DoesExist(workingSession.Domme, co.LeftSide))
                    co.LeftSide = _variableAccessor.GetVariable(workingSession.Domme, co.LeftSide).Value;

                if (_variableAccessor.DoesExist(workingSession.Domme, co.RightSide))
                    co.RightSide = _variableAccessor.GetVariable(workingSession.Domme, co.RightSide).Value;

                if (co.LeftSide.StartsWith("#Session"))
                    co.LeftSide = _sessionVocabularyProcessor.ReplaceVocabulary(workingSession, co.LeftSide);

                if (co.RightSide.StartsWith("#Session"))
                    co.RightSide = _sessionVocabularyProcessor.ReplaceVocabulary(workingSession, co.RightSide);
            });

            var evaluated = parseObjects.Value.Select(co => _conditionalObjectLogic.Evaluate(co)).ToList();
            var lastMatch = evaluated.FindLastIndex(e => e.GetResultOrDefault());
            if (lastMatch == -1)
                return Result.Ok(workingSession);

            var lastTrue = parseObjects.Value[lastMatch];
            var bookmark= _bookmarkService.FindBookmark(workingSession.CurrentScript.Lines, lastTrue.Target)
                .OnSuccess(ln => workingSession.CurrentScript.LineNumber = ln)
                .Map(ln => workingSession)
                .OnSuccess(sesh => OnCommandProcessed(sesh)); ;
            return bookmark;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return ParseObjects(line)
                .OnSuccess(po =>
                {
                    var errors = po.Select(o => _bookmarkService.FindBookmark(script.Lines, o.Target).GetErrorMessageOrDefault())
                        .Where(e => !string.IsNullOrWhiteSpace(e));

                    if (errors.Any())
                        return Result.Fail(string.Join(Environment.NewLine, errors));
                    return Result.Ok();
                });
        }

        private Result<List<ConditionalObject>> ParseObjects(string line)
        {
            var regexMatch = @"@If\[(.*)\](.*)\[(.*)\]Then\((.*)\)";
            var data = Result.Ok(line.Split(' ').Where(l => l.StartsWith(Keyword.If)).ToList());
            var step2 = data
                .Ensure(la => la.All(l => Regex.IsMatch(l, regexMatch)), "Unable to parse an If statement in " + line);
            var step3 = step2
                .OnSuccess(la => la.Select(l =>
                {
                    var group = Regex.Match(l, regexMatch).Groups;
                    return new ConditionalObject
                    {
                        String = @"@If[" + group[1].ToString() + "]" + group[2].ToString() + "[" + group[3].ToString() + "]Then(" + group[4].ToString() + ")",
                        LeftSide = group[1].ToString(),
                        Operator = group[2].ToString(),
                        RightSide = group[3].ToString(),
                        Target = "(" + group[4].ToString() + ")",
                    };
                }).ToList());
            return step3;
        }


    }
}
