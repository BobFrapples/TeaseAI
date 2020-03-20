using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowCaptionsImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowCaptionsImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowCaptionsImage, ImageGenre.Captions, lineService, imageAccessor, randomNumberService)
        {
        }

    }
}