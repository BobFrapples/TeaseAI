using System;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
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
        /// The Command keyword this processor should handle
        /// </summary>
        protected abstract string Keyword { get; }
        /// <summary>
        /// The genre of image this processor should handle
        /// </summary>
        protected abstract ImageGenre Genre { get; }

        public ShowImageCommandProcessorBase(IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
        }

        public override string DeleteCommandFrom(string line) => string.IsNullOrWhiteSpace(line) ? string.Empty : line.Replace(Keyword, string.Empty).Trim();

        public override bool IsRelevant(Session session, string line) => string.IsNullOrWhiteSpace(line) ? false : line.Contains(Keyword);

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var doCommand = _imageAccessor.GetImageMetaDataList(default(ImageSource?), Genre)
                .Ensure(mdl => mdl.Count > 0, ErrorMessage.NoImagesFound)
                .OnSuccess(mdl => mdl[new Random().Next(mdl.Count)])
                .OnSuccess(img => OnCommandProcessed(session, img))
                .Map(img => session);

            return doCommand;
        }

        private readonly IImageAccessor _imageAccessor;
    }
}
