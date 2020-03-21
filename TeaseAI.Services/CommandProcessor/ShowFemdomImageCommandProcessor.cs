using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowFemdomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowFemdomImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowFemdomImage, ImageGenre.Femdom, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
