using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowGeneralImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowGeneralImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowGeneralImage, ImageGenre.General, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}