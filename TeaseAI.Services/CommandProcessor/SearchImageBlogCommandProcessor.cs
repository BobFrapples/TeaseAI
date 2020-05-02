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

        public event EventHandler<CommandProcessedEventArgs> BeforeCommandProcessed;
        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.SearchImageBlog, string.Empty).Replace(Keyword.SearchImageBlogAgain, string.Empty);
        }

        public bool IsRelevant(Session session, string line) => IsRelevant(line);

        public bool IsRelevant(string line) => line.Contains(Keyword.SearchImageBlog) || line.Contains(Keyword.SearchImageBlogAgain);

        public Result<Session> PerformCommand(Session session, string line)
        {
            var images = _imageAccessor.Get(ImageSource.Remote, ImageGenre.Blog);
            var selected = images[new Random().Next(images.Count)];

            OnCommandProcessed(session, selected);

            return Result.Ok(session);
        }

        private void OnCommandProcessed(Session session, ImageMetaData selected)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = selected });
        }

        public Result ParseCommand(Script script, string personalityName, string line)
        {
            if (line.Contains(Keyword.SearchImageBlogAgain))
                return Result.Fail(Keyword.SearchImageBlogAgain + " is deprecated, please use " + Keyword.SearchImageBlog + " instead");
            return Result.Ok();
        }


        private readonly IImageAccessor _imageAccessor;
    }
}