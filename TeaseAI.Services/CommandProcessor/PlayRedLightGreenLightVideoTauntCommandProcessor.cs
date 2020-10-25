using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class PlayRedLightGreenLightVideoTauntCommandProcessor : CommandProcessorBase
    {
        public PlayRedLightGreenLightVideoTauntCommandProcessor(LineService lineService
            , IVideoAccessor videoAccessor
            , ISettingsAccessor settingsAccessor) : base(Keyword.PlayRedLightGreenLight, lineService)
        {
            _videoAccessor = videoAccessor;
            _settingsAccessor = settingsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            workingSession.Phase = workingSession.Phase == SessionPhase.BeforeSession ? SessionPhase.End : workingSession.Phase;
            var script = new Script(new ScriptMetaData
            {
                Info = "Compiled",
                SessionPhase = workingSession.Phase == SessionPhase.BeforeSession ? SessionPhase.End : SessionPhase.Modules,
            },
            new List<string>
            {
                "@PlayVideo"
                , "@RapidTextOn"
                , "(GreenLight)"
                , "@TauntFromFile(Video\\Red Light Green Light\\Green Light.txt)"
                , "@UnpauseVideo"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@If[{Settings.Range.VideoTauntFrequency}] <= [@RandomNumber(100)], SkipTaunt)"
                , "@TauntFromFile(Video\\Red Light Green Light\\Taunts.txt)"
                , "(SkipTaunt)"
                , "@Wait(@RandomNumber({Settings.Range.GreenLightMinimumSeconds},{Settings.Range.GreenLightMaximumSeconds}))"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "(RedLight)"
                , "@TauntFromFile(Video\\Red Light Green Light\\Red Light.txt)"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@PauseVideo"
                , "@Wait(@RandomNumber({Settings.Range.RedLightMinimumSeconds},{Settings.Range.RedLightMaximumSeconds}))"
                , "@If[{Session.IsVideoPlaying}]==[true]Then(GreenLight)"
                , "(End)"
                , "@RapidTextOff"
                , "@End"
                , "@Info Script used by the Red Light / Green Light video tease"
            });

            OnCommandProcessed(workingSession, script);

            workingSession.IsVideoTaunt = true;

            return Result.Ok(workingSession);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _videoAccessor.GetVideoData(default(VideoGenre?))
                .OnSuccess(vids => vids.Where(vmd => vmd.Genre != VideoGenre.CockHero && vmd.Genre != VideoGenre.Joi).Count())
                .Ensure(cnt => cnt > 0, "Porn videos are missing")
                .Map();
        }

        private readonly IVideoAccessor _videoAccessor;
        private readonly ISettingsAccessor _settingsAccessor;

    }
}
