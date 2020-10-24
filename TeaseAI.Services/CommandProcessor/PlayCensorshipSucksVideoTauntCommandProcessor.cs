using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class PlayCensorshipSucksVideoTauntCommandProcessor : CommandProcessorBase
    {
        public PlayCensorshipSucksVideoTauntCommandProcessor(LineService lineService
            , IVideoAccessor videoAccessor
            , ISettingsAccessor settingsAccessor) : base(Keyword.PlayCensorshipSucks, lineService)
        {
            _videoAccessor = videoAccessor;
            _settingsAccessor = settingsAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var script = new Script(new ScriptMetaData
            {
                Info = "Compiled",
                SessionPhase = session.Phase == SessionPhase.BeforeSession ? SessionPhase.End : SessionPhase.Modules,
            },
            new List<string>
            {
                "@PlayVideo"
                , "@RapidTextOn"
                , "(Begin)"
                , "@ShowCensorshipBar"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@Wait(@RandomNumber({Settings.Range.CensorshipBarOnMinimum},{Settings.Range.CensorshipBarOnMaximum}))"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@Chance50(HideBar)"
                , "@TauntFromFile(Video\\Censorship Sucks\\CensorBarOn.txt)"
                , "(HideBar)"
                , "@HideCensorshipBar"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@Wait(@RandomNumber({Settings.Range.CensorshipBarOffMinimum},{Settings.Range.CensorshipBarOffMaximum}))"
                , "@If[{Session.IsVideoPlaying}]==[false]Then(End)"
                , "@Chance50(Begin)"
                , "@TauntFromFile(Video\\Censorship Sucks\\CensorBarOff.txt)"
                , "@If[{Session.IsVideoPlaying}]==[true]Then(Begin)"
                , "(End)"
                , "@HideCensorshipBar"
                , "@RapidTextOff"
                , "@End"
                , "@Info Script used by the Censorship Sucks video tease"
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
