using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Process image commands for both ShowButtImage 
    /// </summary>
    public class ShowButtImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowButtImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowButtImage, ImageGenre.Butt, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}