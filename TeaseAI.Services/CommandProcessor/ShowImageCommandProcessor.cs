﻿using System;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowImageCommandProcessor : CommandProcessorBase
    {
        public ShowImageCommandProcessor(IImageAccessor imageAccessor
            , LineService lineService
            , IRandomNumberService randomNumberService) : base(Keyword.ShowImage, lineService)
        {
            _imageAccessor = imageAccessor;
        }

        public override Result<Session> PerformCommand(Session session, string line)
        {
            var images = _imageAccessor.Get(ImageSource.Local, default(ImageGenre?));
            var selected = images[new Random().Next(images.Count)];

            OnCommandProcessed(session, selected);

            return Result.Ok(session);
        }

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line)
        {
            var images = _imageAccessor.Get(ImageSource.Local, default(ImageGenre?));

            return Result.Ok(images)
                .Ensure(imd => imd.Count() > 0, Keyword.ShowImage + " requires at least one image.")
                .Map();
        }

        private readonly IImageAccessor _imageAccessor;
    }
}
