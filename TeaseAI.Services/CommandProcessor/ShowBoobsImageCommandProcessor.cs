﻿using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowBoobsImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowBoobsImageCommandProcessor(IImageAccessor imageAccessor) : base(imageAccessor)
        {
        }

        protected override ImageGenre Genre => ImageGenre.Boobs;

        protected override string Keyword => Common.Constants.Keyword.ShowBoobImage;
    }
}