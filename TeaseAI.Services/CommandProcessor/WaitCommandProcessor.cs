using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class WaitCommandProcessor : CommandProcessorBase
    {
        public WaitCommandProcessor(LineService lineService) : base(Keyword.Wait, lineService)
        {
            _lineService = lineService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            return GetWaitTime(line)
                .OnSuccess(time =>
                {
                    Task.Delay(time * 1000).Wait();
                    return Result.Ok(session);
                });
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return GetWaitTime(line)
                .Map();
        }

        private Result<int> GetWaitTime(string line)
        {
            var data = _lineService.GetParenData(line, Keyword.Wait)
                .Ensure(pd => pd.Count == 1, "CurrentLine is invalid")
                .Map(pd => pd[0].ToLower())
                .OnSuccess(time =>
                {
                    var units = 1; // Seconds
                    if (time.Contains("m"))
                        units = 60; // minutes
                    else if (time.Contains("h"))
                        units = 3600; // hours
                    var waitCount = 0;
                    if (!Int32.TryParse(time.Replace("m", "").Replace("h", ""), out waitCount))
                        return Result.Fail<int>(time + " is an invalid time.");
                    return Result.Ok(waitCount * units);
                });
            return data;
        }
        private LineService _lineService;
    }
}
