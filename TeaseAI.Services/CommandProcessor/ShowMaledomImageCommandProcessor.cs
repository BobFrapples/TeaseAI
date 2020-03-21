using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowMaledomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowMaledomImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowMaledomImage, ImageGenre.Maledom, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}