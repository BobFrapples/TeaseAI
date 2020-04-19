using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Services
{
    public class MediaContainerService : IMediaContainerService
    {
        private readonly IMediaContainerRepository _mediaContainerRepository;
        private readonly IGenreRepository _genreRepository;

        public MediaContainerService(IMediaContainerRepository mediaContainerRepository
            , IGenreRepository genreRepository)
        {
            _mediaContainerRepository = mediaContainerRepository;
            _genreRepository = genreRepository;
        }

        public Result<MediaContainer> Create(MediaContainer mediaContainer) => _mediaContainerRepository.Create(mediaContainer);

        public Result<List<MediaContainer>> Create(List<MediaContainer> mediaContainers)
        {
            var results = new List<MediaContainer>();
            foreach (var mediaContainer in mediaContainers)
            {
                var createContainer = _mediaContainerRepository.Create(mediaContainer);
                if (createContainer.IsFailure)
                    return Result.Fail<List<MediaContainer>>(createContainer.Error);
                results.Add(createContainer.Value);
            }

            return Result.Ok(results);
        }

        public List<MediaContainer> Get() => _mediaContainerRepository.Get();

        public Result<MediaContainer> Get(int containerId) => _mediaContainerRepository.Get(containerId);

        public List<MediaContainer> Get(int mediaTypeId, ImageSource imageSource) =>
            _mediaContainerRepository.Get().Where(mc => mc.MediaTypeId == mediaTypeId && mc.SourceId == imageSource).ToList();

        public Result<List<MediaContainer>> Update(List<MediaContainer> mediaContainers)
        {
            var results = new List<MediaContainer>();
            foreach (var mediaContainer in mediaContainers)
            {
                var updateContainer =_mediaContainerRepository.Update(mediaContainer);
                if (updateContainer.IsFailure)
                    return Result.Fail<List<MediaContainer>>(updateContainer.Error);
            }
            return Result.Ok(results);
        }

        private MediaContainer CreateMediaContainer(int id, string name, ImageGenre genre, int mediaType, ImageSource source)
        {
            return new MediaContainer
            {
                Id = id,
                Name = name,
                GenreId = genre,
                IsEnabled = false,
                MediaTypeId = mediaType, // needs constants, 1 is image ,
                Path = string.Empty,
                SourceId = source,
                UseSubFolders = false
            };
        }
    }
}
