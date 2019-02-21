using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Events;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// process both ShowLocalImage and ShowLocalCategoryImage
    /// </summary>
    public class ShowLocalImageCommandProcessor : ICommandProcessor
    {
        public ShowLocalImageCommandProcessor(IImageAccessor imageAccessor, LineService lineService)
        {
            _imageAccessor = imageAccessor;
            _lineService = lineService;
        }

        public event EventHandler<CommandProcessedEventArgs> CommandProcessed;

        public string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.ShowLocalImage, string.Empty).Replace(Keyword.ShowLocalCategoryImage, string.Empty);
        }

        public bool IsRelevant(Session session, string line) => line.Contains(Keyword.ShowLocalImage);

        public Result<Session> PerformCommand(Session session, string line)
        {
            var genres = line.Contains(Keyword.ShowLocalCategoryImage) ? GetCategoryies(line) : new List<string>();
            return _imageAccessor.GetImageMetaDataList(ImageSource.Local, null)
                .Ensure(data => data.Count > 0, "No  images of genre " + ImageGenre.Boobs.ToString() + " to load")
                .OnSuccess(data => data[new Random().Next(data.Count)])
                .OnSuccess(img => OnCommandProcessed(session, img))
                .Map(img => session);
        }

        private List<string> GetCategoryies(string line)
        {
            var data = _lineService.GetParenData(line, Keyword.ShowLocalCategoryImage);
            return data.GetResultOrDefault();
        }

        private void OnCommandProcessed(Session session, ImageMetaData selected)
        {
            CommandProcessed?.Invoke(this, new CommandProcessedEventArgs() { Session = session, Parameter = selected });
        }

        private readonly IImageAccessor _imageAccessor;
        private readonly LineService _lineService;
    }
}
