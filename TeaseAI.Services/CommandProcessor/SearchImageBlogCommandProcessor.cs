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
    public class SearchImageBlogCommandProcessor : ICommandProcessor
    {
        public SearchImageBlogCommandProcessor(IImageAccessor imageAccessor)
        {
            _imageAccessor = imageAccessor;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.SearchImageBlog, string.Empty).Replace(Keyword.SearchImageBlogAgain, string.Empty);
        }

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.SearchImageBlog) || line.Contains(Keyword.SearchImageBlogAgain);

        public Result<Session> PerformCommand(Session session, string line)
        {
            Result<List<ImageMetaData>> images = _imageAccessor.GetImageMetaDataList(ImageSource.Remote, ImageGenre.Blog);
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