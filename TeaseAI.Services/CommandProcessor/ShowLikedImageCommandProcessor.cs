using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowLikedImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowLikedImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowLikedImage, ImageGenre.Liked, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}