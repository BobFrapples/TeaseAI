using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowDislikedImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowDislikedImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowDislikedImage, ImageGenre.Disliked, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}