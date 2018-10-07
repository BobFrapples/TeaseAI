using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services.CommandProcessor
{
    public class WaitCommandProcessor : ICommandProcessor
    {
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public bool IsRelevant(Session session, string line)
        {
            return session.CurrentScript.CurrentLine.Contains(Keyword.Wait);
        }

        public Result<Session> PerformCommand(Session session, string line)
        {
            var ls = new LineService();
            var data = ls.GetParenData(session.CurrentScript.CurrentLine, Keyword.Wait)
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
                         return Result.Fail<Session>(time + " is an invalid time.");
                     Task.Delay(waitCount * units * 1000).Wait();

                     return Result.Ok(session);
                 });
            return data;
        }

        public string DeleteCommandFrom(string line)
        {
            var rgx = new Regex(Keyword.Wait + ".*" + ")");
            return line;
        }
    }
}
