﻿using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class RiskyPickSelectCaseCommandProcessor : CommandProcessorBase
    {
        public RiskyPickSelectCaseCommandProcessor(LineService lineService) : base(Keyword.RiskyPickSelectCase, lineService)
        {
            _lineService = lineService;
        }

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.RiskyPickSelectCase) && session.GameBoard != null;


        public override Result<Session> PerformCommand(Session session, string line)
        {
            return _lineService.GetParenData(line, Keyword.RiskyPickSelectCase)
                .Ensure(l => l.Count == 1, "Too many parameters passed to " + Keyword.RiskyPickSelectCase + " in " + line)
                .OnSuccess(l => Convert.ToInt16(l[0]))
                .OnSuccess(cn =>
                {
                    if (session.GameBoard.Cases[cn].IsOpened)
                        Result.Fail<int>("Case number " + cn.ToString() + " is already opened.");
                    return cn;
                })
                .OnSuccess(cn =>
                {
                    var workingSession = session.Clone();

                    if (workingSession.GameBoard.CurrentRound == 0)
                        workingSession.GameBoard.PlayersCase = workingSession.GameBoard.Cases[cn];
                    else
                    {
                        workingSession.GameBoard.Cases[cn].IsOpened = true;
                        workingSession.GameBoard.SelectedCases.Add(cn);
                    }
                    OnCommandProcessed(workingSession);

                    return workingSession;
                });
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _lineService.GetParenData(line, Keyword.RiskyPickSelectCase)
                .Ensure(l => l.Count == 1, "Too many parameters passed to " + Keyword.RiskyPickSelectCase + " in " + line)
                .Ensure(l => int.TryParse(l[0], out var toss), Keyword.RiskyPickSelectCase + " requires inputs be a whole number")
                .Map();
        }
    }
}
