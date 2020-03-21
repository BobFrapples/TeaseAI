using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowSoftcoreImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowSoftcoreImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowSoftcoreImage, ImageGenre.Softcore, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
