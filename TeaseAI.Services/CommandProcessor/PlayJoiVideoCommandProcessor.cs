using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class PlayJoiVideoCommandProcessor : CommandProcessorBase
    {
        public PlayJoiVideoCommandProcessor(IVideoAccessor videoAccessor)
        {
            _videoAccessor = videoAccessor;
        }

        public override string DeleteCommandFrom(string line) => line.Replace(Keyword.PlayJoiVideo, string.Empty);

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.PlayJoiVideo);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var getVideos = _videoAccessor.GetVideoData(default(VideoGenre?));

            var videos = getVideos.Value.Where(vmd =>vmd.Genre == VideoGenre.Joi).ToList();

            var selected = videos[new Random().Next(videos.Count)];

            var ea = new PlayVideoEventArgs()
            {
                ShouldRandomizeStart = false,
                VideoMetaData = selected,
            };

            OnCommandProcessed(workingSession, ea);

            if (ea.Result.IsSuccess)
                return Result.Ok(workingSession);

            return Result.Fail<Session>(ea.Result.Error);
        }

        private readonly IVideoAccessor _videoAccessor;
    }
}
