using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class DislikeImageCommandProcessor : CommandProcessorBase
    {
        public DislikeImageCommandProcessor(LineService lineService
          , IImageAccessor imageAccessor) : base(Keyword.DislikeImage, lineService)
        {
            _imageAccessor = imageAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var showImageEventArgs = new ShowImageEventArgs();

            OnCommandProcessed(session, showImageEventArgs);

            if (showImageEventArgs.ImageMetaData != null)
            {
                var likedImages = _imageAccessor.Get(default(ImageSource?), ImageGenre.Disliked);
                if (likedImages.All(imd => imd.ItemName != showImageEventArgs.ImageMetaData.ItemName))
                {
                    var likedImage = showImageEventArgs.ImageMetaData.Clone();
                    likedImage.GenreId = ImageGenre.Disliked;
                    likedImages.Add(showImageEventArgs.ImageMetaData);
                }
                Result save = _imageAccessor.Update(likedImages);

            }

            return Result.Ok(session);
        }

        /// <summary>
        /// This command is completely session dependant.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="personalityName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private readonly IImageAccessor _imageAccessor;
    }
}
