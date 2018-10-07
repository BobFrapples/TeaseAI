using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowHardcoreImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowHardcoreImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Hardcore;

        protected override string Keyword => Common.Constants.Keyword.ShowHardcoreImage;
    }
}
