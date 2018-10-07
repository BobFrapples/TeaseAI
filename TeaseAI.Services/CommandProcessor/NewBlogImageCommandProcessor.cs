using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class NewBlogImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public NewBlogImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Blog;

        protected override string Keyword => Common.Constants.Keyword.NewBlogImage;
    }
}
