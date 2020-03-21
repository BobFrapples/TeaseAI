using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class NewBlogImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public NewBlogImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService
            ) : base(Common.Constants.Keyword.NewBlogImage, ImageGenre.Blog, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}
