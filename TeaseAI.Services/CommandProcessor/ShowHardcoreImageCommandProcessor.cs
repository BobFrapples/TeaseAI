using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowHardcoreImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowHardcoreImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowHardcoreImage, ImageGenre.Hardcore, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
