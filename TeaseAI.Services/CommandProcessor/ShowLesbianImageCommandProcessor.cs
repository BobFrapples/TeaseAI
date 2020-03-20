using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLesbianImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLesbianImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowLesbianImage, ImageGenre.Lesbian, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
