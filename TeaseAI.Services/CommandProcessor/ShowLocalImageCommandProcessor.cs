using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.CommandProcessor
{
    /// <summary>
    /// process both ShowLocalImage and ShowLocalCategoryImage
    /// </summary>
    public class ShowLocalImageCommandProcessor : CommandProcessorBase
    {
        public ShowLocalImageCommandProcessor(IImageAccessor imageAccessor, LineService lineService) : base(Keyword.ShowLocalImage, lineService)
        {
            _imageAccessor = imageAccessor;
            _lineService = lineService;
        }

        public override string DeleteCommandFrom(string line)
        {
            return line.Replace(Keyword.ShowLocalImage, string.Empty).Replace(Keyword.ShowLocalCategoryImage, string.Empty);
        }

        public override bool IsRelevant(Session session, string line) => line.Contains(Keyword.ShowLocalImage);

        public override Result<Session> PerformCommand(Session session, string line)
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

        protected override Result ParseCommandSpecific(Script script, string personalityName, string line) => Result.Ok();

        private readonly IImageAccessor _imageAccessor;
        private readonly LineService _lineService;
    }
}
