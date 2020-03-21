using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowBlowjobImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowBlowjobImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowBlowjobImage, ImageGenre.Blowjob, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
