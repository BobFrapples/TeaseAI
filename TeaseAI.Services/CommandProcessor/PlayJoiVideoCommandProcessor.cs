using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class PlayJoiVideoCommandProcessor : CommandProcessorBase
    {
        public PlayJoiVideoCommandProcessor(LineService lineService
            , IVideoAccessor videoAccessor
            , IRandomNumberService randomNumberService) : base(Keyword.PlayJoiVideo, lineService)
        {
            _videoAccessor = videoAccessor;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var workingSession = session.Clone();
            var getVideos = _videoAccessor.GetVideoData(VideoGenre.Joi);
            var videos = getVideos.Value;
            var selected = videos[_randomNumberService.Roll(0, videos.Count)];

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

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return _videoAccessor.GetVideoData(VideoGenre.Joi)
                .Ensure(vids => vids.Count() > 0, "There are no Jerk Off Instruction videos found")
                .Map();
        }

        private readonly IVideoAccessor _videoAccessor;
        private readonly IRandomNumberService _randomNumberService;
    }
}
