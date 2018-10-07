using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowBlowjobImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowBlowjobImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Blowjob;

        protected override string Keyword => Common.Constants.Keyword.ShowBlowjobImage;
    }
}
