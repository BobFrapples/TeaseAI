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
    public class PlayVideoCommandProcessor : ICommandProcessor
    {
        public PlayVideoCommandProcessor(IVideoAccessor videoAccessor)
        {
            _videoAccessor = videoAccessor;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.PlayVideo, string.Empty).Replace(Keyword.JumpVideo, string.Empty);
        }

        public bool IsRelevant(Session session, string line)
        {
            return line.Contains(Keyword.PlayVideo) && !line.Contains(Keyword.PlaySpecificVideo) && !line.Contains(Keyword.PlaySpecificVideoSquareBrackets);
        }

        public Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();

            Result<List<VideoMetaData>> getVideos = _videoAccessor.GetVideoData(default(VideoGenre?));

            var videos = getVideos.Value.Where(vmd => vmd.Genre != VideoGenre.CockHero && vmd.Genre != VideoGenre.Joi).ToList();

            var selected = videos[new Random().Next(videos.Count)];

            var ea = new PlayVideoEventArgs()
            {
                ShouldRandomizeStart = line.Contains(Keyword.JumpVideo),
                VideoMetaData = selected,
            };

            OnCommandProcessed(workingSession, ea);

            if (ea.Result.IsSuccess)
                return Result.Ok(workingSession);
            return Result.Fail<Session>(ea.Result.Error);
        }

        private void OnCommandProcessed(Session session, PlayVideoEventArgs selected)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = selected });
        }

        private readonly IVideoAccessor _videoAccessor;
    }
}
