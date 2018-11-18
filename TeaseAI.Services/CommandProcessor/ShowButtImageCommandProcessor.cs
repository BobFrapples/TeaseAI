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
    public class ShowButtImageCommandProcessor : ICommandProcessor
    {
        public ShowButtImageCommandProcessor(IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.ShowButtImage, string.Empty).Replace(Keyword.ShowButtsImage, string.Empty);
        }

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.ShowButtImage) || line.Contains(Keyword.ShowButtsImage);

        public Result<Session> PerformCommand(Session session, string line)
        {
            return _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Butt)
                .Ensure(data => data.Count > 0, "No  images of genre " + ImageGenre.Boobs.ToString() + " to load")
                .OnSuccess(data => data[new Random().Next(data.Count)])
                .OnSuccess(img => OnCommandProcessed(session, img))
                .Map(img => session);
        }

        private void OnCommandProcessed(Session session, ImageMetaData selected)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = selected });
        }

        private readonly IImageAccessor _imageAccessor;
    }
}