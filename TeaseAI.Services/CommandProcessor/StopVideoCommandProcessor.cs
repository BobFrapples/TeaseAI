using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Services.CommandProcessor
{
    public class StopVideoCommandProcessor:CommandProcessorBase
    {
        public StopVideoCommandProcessor(LineService lineService) : base(Keyword.StopVideo, lineService)
        {
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.VideoPlaying = null;
            workingSession.IsVideoTaunt = false;

            OnCommandProcessed(workingSession);

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return Result.Ok();
        }
    }
}
