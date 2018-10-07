using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowFemdomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowFemdomImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Femdom;

        protected override string Keyword => Common.Constants.Keyword.ShowFemdomImage;
    }
}
