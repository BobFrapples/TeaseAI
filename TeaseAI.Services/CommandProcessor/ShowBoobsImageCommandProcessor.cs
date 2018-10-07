﻿using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    public class ShowBoobsImageCommandProcessor : ICommandProcessor
    {
        public ShowBoobsImageCommandProcessor(IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.ShowBoobImage, string.Empty).Replace(Keyword.ShowBoobsImage, string.Empty);
        }

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.ShowBoobImage) || line.Contains(Keyword.ShowBoobsImage);

        public Result<Session> PerformCommand(Session session, string line)
        {
            Result<List<ImageMetaData>> images = _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Boobs);
            var selected = images.Value[new Random().Next(images.Value.Count)];

            OnCommandProcessed(session, selected);

            return Result.Ok(session);
        }

        private void OnCommandProcessed(Session session, ImageMetaData selected)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = selected });
        }

        private readonly IImageAccessor _imageAccessor;
    }
}