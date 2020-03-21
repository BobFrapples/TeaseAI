using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowBlogImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowBlogImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowBlogImage, ImageGenre.Blog, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
