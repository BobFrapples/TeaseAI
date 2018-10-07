using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLesbianImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLesbianImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Lesbian;

        protected override string Keyword => Common.Constants.Keyword.ShowLesbianImage;
    }
}
