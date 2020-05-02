using System;
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
    /// Proceess the image liked command
    /// </summary>
    public class LikeImageCommandProcessor : CommandProcessorBase
    {
        public LikeImageCommandProcessor(LineService lineService
            , IImageAccessor imageAccessor
            , IMediaContainerService mediaContainerService) : base(Keyword.LikeImage, lineService)
        {
            _imageAccessor = imageAccessor;
            _mediaContainerService = mediaContainerService;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var showImageEventArgs = new ShowImageEventArgs();

            OnBeforeCommandProcessed(session, showImageEventArgs);

            if (showImageEventArgs.ImageMetaData != null)
            {
                var li = _imageAccessor.Get(default(ImageSource?), ImageGenre.Liked);
                if (li.All(imd => imd.ItemName != showImageEventArgs.ImageMetaData.ItemName))
                {
                    var likedImage = showImageEventArgs.ImageMetaData.Clone();
                    likedImage.GenreId = ImageGenre.Liked;
                    li.Add(showImageEventArgs.ImageMetaData);
                }
                Result save = _imageAccessor.Update(li);

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
        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            return Result.Ok(_mediaContainerService.Get().Where(mc => mc.MediaTypeId == 1 && mc.IsEnabled && (mc.SourceId == ImageSource.Local || mc.SourceId == ImageSource.Remote)))
                 .Ensure(mc => mc.Any(), "This command requires either remote or local images to be setup")
                 .Map();
        }

        private readonly IImageAccessor _imageAccessor;
        private readonly IMediaContainerService _mediaContainerService;
    }
}
