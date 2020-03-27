using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Base Image command processor that implements <see cref="CommandProcessorBase"/>
    /// children only have to define the command, and the genre
    /// </summary>
    public abstract class ShowImageCommandProcessorBase : CommandProcessorBase
    {
        /// <summary>
        /// The genre of image this processor should handle
        /// </summary>
        protected ImageGenre Genre { get; }

        /// <summary>
        /// The Command keyword this processor should handle
        /// </summary>
        protected string Keyword { get; }

        public ShowImageCommandProcessorBase(string keyword
            , ImageGenre imageGenre
            , LineService lineService
            , IImageAccessor imageAccessor
            , IRandomNumberService randomNumberService
            ) : base(keyword, lineService)
        {
            Keyword = keyword;
            _lineService = lineService;
            _imageAccessor = imageAccessor;
            _randomNumberService = randomNumberService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var doCommand = Result.Ok(_imageAccessor.Get(default(ImageSource?), Genre))
                .Ensure(mdl => mdl.Count > 0, ErrorMessage.NoImagesFound)
                .OnSuccess(mdl => mdl[_randomNumberService.Roll(0, mdl.Count)])
                .OnSuccess(img => OnCommandProcessed(session, img))
                .Map(img => session);

            return doCommand;
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private readonly IImageAccessor _imageAccessor;
        private readonly IRandomNumberService _randomNumberService;
        private readonly LineService _lineService;
    }
}
