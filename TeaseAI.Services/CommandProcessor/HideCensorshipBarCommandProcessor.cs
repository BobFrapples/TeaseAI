using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class HideCensorshipBarCommandProcessor : CommandProcessorBase
    {
        public HideCensorshipBarCommandProcessor(LineService lineService) : base(Keyword.HideCensorshipBar, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            OnCommandProcessed(session, false);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return Result.Ok();
        }
    }
}
