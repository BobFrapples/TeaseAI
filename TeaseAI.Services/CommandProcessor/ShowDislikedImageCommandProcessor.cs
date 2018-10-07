using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowDislikedImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowDislikedImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Disliked;

        protected override string Keyword => Common.Constants.Keyword.ShowDislikedImage;
    }
}