using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowCaptionsImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowCaptionsImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Captions;

        protected override string Keyword => Common.Constants.Keyword.ShowCaptionsImage;
    }
}