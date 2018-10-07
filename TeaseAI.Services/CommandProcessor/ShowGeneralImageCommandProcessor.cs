using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowGeneralImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowGeneralImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.General;

        protected override string Keyword => Common.Constants.Keyword.ShowGeneralImage;
    }
}