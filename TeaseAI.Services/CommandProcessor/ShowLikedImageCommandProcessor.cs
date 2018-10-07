using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLikedImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLikedImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Liked;

        protected override string Keyword => Common.Constants.Keyword.ShowLikedImage;
    }
}