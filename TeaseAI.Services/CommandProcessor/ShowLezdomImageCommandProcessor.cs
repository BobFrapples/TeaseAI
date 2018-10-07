using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLezdomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLezdomImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Lezdom;

        protected override string Keyword => Common.Constants.Keyword.ShowLezdomImage;
    }
}