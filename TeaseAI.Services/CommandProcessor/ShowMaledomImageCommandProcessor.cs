﻿using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowMaledomImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowMaledomImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Maledom;

        protected override string Keyword => Common.Constants.Keyword.ShowMaledomImage;
    }
}