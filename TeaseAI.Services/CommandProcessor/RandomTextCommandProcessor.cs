﻿using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;


namespace TeaseAI.Services.CommandProcessor
{
    public class RandomTextCommandProcessor : ICommandProcessor
    {
        public RandomTextCommandProcessor(LineService lineService)
        {
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> BeforeCommandProcessed;
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        /// <summary>
        /// This processor actually does its work in the DeleteCommandFrom method
        /// </summary>
        /// <param name="session"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public Result<Session> PerformCommand(Session session, string line)
        {
            return Result.Ok(session);
        }

        public string DeleteCommandFrom(string input)
        {
            var command = input.Contains(Keyword.RandomText) ? Keyword.RandomText : Keyword.RT;
            var result = _lineService.GetParenData(input, command)
                .OnSuccess(pd => pd[new Random().Next(pd.Count)])
                .OnSuccess(text => _lineService.ReplaceParenData(input, command, text));


            return result.Value;
        }

        public bool IsRelevant(Session session, string line) => IsRelevant(line);

        public bool IsRelevant(string line) => line.Contains(Keyword.RT) || line.Contains(Keyword.RandomText);

        void OnCommandProcessed(Session session)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, });
        }

        public Result ParseCommand(Script script, string personalityName, string line)
        {
            if (line.Contains(Keyword.RT))
                return Result.Fail(Keyword.RT + " is deprecated, please use " + Keyword.RandomText);

            return _lineService.GetParenData(line, Keyword.RandomText)
                .Ensure(pd => pd.Count > 0, Keyword.RandomText + " requires at least one parameter")
                .Map();
        }


        private LineService _lineService;
    }
}
