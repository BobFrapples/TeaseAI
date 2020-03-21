using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLezdomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLezdomImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowLezdomImage, ImageGenre.Lezdom, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}