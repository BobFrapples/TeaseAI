using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowHentaiImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowHentaiImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Hentai;

        protected override string Keyword => Common.Constants.Keyword.ShowHentaiImage;
    }
}