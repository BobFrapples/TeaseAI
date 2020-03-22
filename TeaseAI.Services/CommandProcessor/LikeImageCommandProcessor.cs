using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class LikeImageCommandProcessor : CommandProcessorBase
    {
        public LikeImageCommandProcessor(LineService lineService
            , IImageAccessor imageAccessor) : base(Keyword.LikeImage, lineService)
        {
            _imageAccessor = imageAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var showImageEventArgs = new ShowImageEventArgs();

            OnCommandProcessed(session, showImageEventArgs);

            if (showImageEventArgs.ImageMetaData != null)
            {
                var getLikedImages = _imageAccessor.GetImageMetaDataList(ImageSource.Local, ImageGenre.Liked)
                    .OnSuccess(li =>
                    {
                        if (li.All(imd => imd.ItemName != showImageEventArgs.ImageMetaData.ItemName))
                        {
                            var likedImage = showImageEventArgs.ImageMetaData.Clone();
                            likedImage.Genre = ImageGenre.Liked;
                            li.Add(showImageEventArgs.ImageMetaData);
                        }
                    });
                Result save = _imageAccessor.SaveImageMetaData(getLikedImages.Value);

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
