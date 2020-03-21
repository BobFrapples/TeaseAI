using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// Process image commands for both ShowBoobImage and ShowBoobsImage
    /// </summary>
    public class ShowBoobsImageCommandProcessor : ShowImageCommandProcessorBase
    {
        public ShowBoobsImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Common.Constants.Keyword.ShowBoobsImage, ImageGenre.Boobs, lineService, imageAccessor, randomNumberService)
        {
        }
    }
}