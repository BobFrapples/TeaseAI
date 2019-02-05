using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowButtImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowButtImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Butt;

        protected override string Keyword => Common.Constants.Keyword.ShowButtImage;
    }
}