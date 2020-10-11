using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Play a random video, will ignore CockHero and Jerk Off Instruction videos
    /// </summary>
    public class PlayVideoCommandProcessor : CommandProcessorBase
    {
        public PlayVideoCommandProcessor(LineService lineService
            , IVideoAccessor videoAccessor) : base(Keyword.PlayVideo, lineService)
        {
            _videoAccessor = videoAccessor;
        }

        public override string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.PlayVideo, string.Empty).Replace(Keyword.JumpVideo, string.Empty);
        }

        public override bool IsRelevant(string line)
        {
            return line.Contains(Keyword.PlayVideo) && !line.Contains(Keyword.PlaySpecificVideo) && !line.Contains(Keyword.PlaySpecificVideoSquareBrackets);
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();

            var getVideos = _videoAccessor.GetVideoData(default(VideoGenre?));
            var videos = getVideos.Value.Where(vmd => vmd.Genre != VideoGenre.CockHero && vmd.Genre != VideoGenre.Joi).ToList();

            var selected = videos[new Random().Next(videos.Count)];

            var ea = new PlayVideoEventArgs()
            {
                ShouldRandomizeStart = line.Contains(Keyword.JumpVideo),
                VideoMetaData = selected,
            };

            OnCommandProcessed(workingSession, ea);

            if (ea.Result.IsFailure)
                return Result.Fail<Session>(ea.Result.Error);

            workingSession.VideoPlaying = selected;
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
    }
}
