using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowGayImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowGayImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowGayImage, ImageGenre.Gay, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
