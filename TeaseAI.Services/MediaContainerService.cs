using System;
using System.Collections.Generic;
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

        public List<MediaContainer> Get() => _mediaContainerRepository.Get();

        public Result<MediaContainer> Get(int containerId) => _mediaContainerRepository.Get(containerId);

        public void Initialize()
        {
            var defaultData = CreateDefaultContainers();
            foreach (var container in defaultData)
            {
                var getGenre = _mediaContainerRepository.Get(container.Id);
                if (getGenre.IsSuccess)
                    _mediaContainerRepository.Update(container);
                else
                    _mediaContainerRepository.Create(container);
            }
        }

        public void Update(List<MediaContainer> mediaContainers)
        {
            foreach (var mediaContainer in mediaContainers)
                _mediaContainerRepository.Update(mediaContainer);
        }

        private List<MediaContainer> CreateDefaultContainers()
        {
            return new List<MediaContainer>
            {
                CreateMediaContainer(1, nameof(ImageGenre.Blog), ImageGenre.Blog,1,ImageSource.Remote),
                CreateMediaContainer(2, nameof(ImageGenre.Blowjob), ImageGenre.Blowjob,1,ImageSource.Local),
                CreateMediaContainer(3, nameof(ImageGenre.Blowjob), ImageGenre.Blowjob,1,ImageSource.Remote),
                CreateMediaContainer(4, nameof(ImageGenre.Boobs), ImageGenre.Boobs,1,ImageSource.Local),
                CreateMediaContainer(5, nameof(ImageGenre.Boobs), ImageGenre.Boobs,1,ImageSource.Remote),
                CreateMediaContainer(6, nameof(ImageGenre.Butt), ImageGenre.Butt,1,ImageSource.Local),
                CreateMediaContainer(7, nameof(ImageGenre.Butt), ImageGenre.Butt,1,ImageSource.Remote),
                CreateMediaContainer(8, nameof(ImageGenre.Hardcore), ImageGenre.Hardcore,1,ImageSource.Local),
                CreateMediaContainer(9, nameof(ImageGenre.Hardcore), ImageGenre.Hardcore,1,ImageSource.Remote),
                CreateMediaContainer(10, nameof(ImageGenre.Softcore), ImageGenre.Softcore,1,ImageSource.Local),
                CreateMediaContainer(11, nameof(ImageGenre.Softcore), ImageGenre.Softcore,1,ImageSource.Remote),
                CreateMediaContainer(12, nameof(ImageGenre.Lesbian), ImageGenre.Lesbian,1,ImageSource.Local),
                CreateMediaContainer(13, nameof(ImageGenre.Lesbian), ImageGenre.Lesbian,1,ImageSource.Remote),
                CreateMediaContainer(14, nameof(ImageGenre.Femdom), ImageGenre.Femdom,1,ImageSource.Local),
                CreateMediaContainer(15, nameof(ImageGenre.Femdom), ImageGenre.Femdom,1,ImageSource.Remote),
                CreateMediaContainer(16, nameof(ImageGenre.Lezdom), ImageGenre.Lezdom,1,ImageSource.Local),
                CreateMediaContainer(17, nameof(ImageGenre.Lezdom), ImageGenre.Lezdom,1,ImageSource.Remote),
                CreateMediaContainer(18, nameof(ImageGenre.Hentai), ImageGenre.Hentai,1,ImageSource.Local),
                CreateMediaContainer(19, nameof(ImageGenre.Hentai), ImageGenre.Hentai,1,ImageSource.Remote),
                CreateMediaContainer(20, nameof(ImageGenre.Gay), ImageGenre.Gay,1,ImageSource.Local),
                CreateMediaContainer(21, nameof(ImageGenre.Gay), ImageGenre.Gay,1,ImageSource.Remote),
                CreateMediaContainer(22, nameof(ImageGenre.Lesbian), ImageGenre.Lesbian,1,ImageSource.Local),
                CreateMediaContainer(23, nameof(ImageGenre.Lesbian), ImageGenre.Lesbian,1,ImageSource.Remote),
                CreateMediaContainer(24, nameof(ImageGenre.Maledom), ImageGenre.Maledom,1,ImageSource.Local),
                CreateMediaContainer(25, nameof(ImageGenre.Maledom), ImageGenre.Maledom,1,ImageSource.Remote),
                CreateMediaContainer(26, nameof(ImageGenre.Captions), ImageGenre.Captions,1,ImageSource.Local),
                CreateMediaContainer(27, nameof(ImageGenre.Captions), ImageGenre.Captions,1,ImageSource.Remote),
                CreateMediaContainer(28, nameof(ImageGenre.General), ImageGenre.General,1,ImageSource.Local),
                CreateMediaContainer(29, nameof(ImageGenre.General), ImageGenre.General,1,ImageSource.Remote),
                CreateMediaContainer(30, nameof(ImageGenre.Liked), ImageGenre.Liked,1,ImageSource.Local),
                CreateMediaContainer(31, nameof(ImageGenre.Liked), ImageGenre.Liked,1,ImageSource.Remote),
                CreateMediaContainer(32, nameof(ImageGenre.Disliked), ImageGenre.Disliked,1,ImageSource.Local),
                CreateMediaContainer(33, nameof(ImageGenre.Disliked), ImageGenre.Disliked,1,ImageSource.Remote),
            };
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
