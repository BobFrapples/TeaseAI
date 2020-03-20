using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowHentaiImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowHentaiImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowHentaiImage, ImageGenre.Hentai, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}