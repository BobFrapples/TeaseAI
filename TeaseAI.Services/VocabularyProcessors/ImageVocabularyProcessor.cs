using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.VocabularyProcessors
{
    public class ImageVocabularyProcessor : BaseVocabularyProcessor
    {
        private readonly IImageAccessor _imageAccessor;

        protected override Dictionary<string, Func<Session, string, string>> Vocabulary { get; set; }

        public ImageVocabularyProcessor(IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
            Vocabulary = new Dictionary<string, Func<Session, string, string>>
            {
                { "#LocalImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, default(ImageGenre?)) },
                { "#BlogImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Blog) },
                { "#ButtImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Butt) },
                { "#ButtsImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Butt) },
                { "#BoobImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Boobs) },
                { "#BoobsImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Boobs) },
                { "#HardCoreImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Hardcore) },
                { "#SoftCoreImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Softcore) },
                { "#LesbianImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Lesbian) },
                { "#BlowjobImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Blowjob) },
                { "#FemdomImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Femdom) },
                { "#LezdomImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Lezdom) },
                { "#HentaiImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Hentai) },
                { "#GayImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Gay) },
                { "#MaledomImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Maledom) },
                { "#CaptionsImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Captions) },
                { "#LikedImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Liked) },
                { "#DislikedImageCount", (session, line) => GetImageCount(session, line, ImageSource.Local, ImageGenre.Disliked) },
            };
        }

        private string GetImageCount(Session session, string line, ImageSource? imageSource, ImageGenre? imageGenre)
        {
            var getCount = _imageAccessor.GetImageMetaDataList(imageSource, imageGenre)
                .OnSuccess(data => data.Count);
            return getCount.IsSuccess ? getCount.Value.ToString() : getCount.Error.Message;
        }
    }
}
