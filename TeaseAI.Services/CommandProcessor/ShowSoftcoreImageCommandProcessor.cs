using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowSoftcoreImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowSoftcoreImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Softcore;

        protected override string Keyword => Common.Constants.Keyword.ShowSoftcoreImage;
    }
}
