using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Services.Accessors
{
    public class ImageAccessor : IImageAccessor
    {
        public ImageAccessor(IConfigurationAccessor configurationAccessor
            , IPathsAccessor pathsAccessor
            , IImageMetaDataRepository imageMetaDataRepository
            , IMediaContainerService mediaContainerService)
        {
            _configurationAccessor = configurationAccessor;
            _pathsAccessor = pathsAccessor;
            _imageMetaDataRepository = imageMetaDataRepository;
            _mediaContainerService = mediaContainerService;
        }

        public List<ImageMetaData> Get(ImageSource? source, ImageGenre? genre)
        {
            return _imageMetaDataRepository.Get(source, genre);
        }

        public Result Update(IEnumerable<ImageMetaData> imageMetaDatas)
        {
            return _imageMetaDataRepository.Update(imageMetaDatas);
        }

        public void Initialize()
        {
            var applicationConfig = _configurationAccessor.GetApplicationConfiguration();
            var mediaContainers = _mediaContainerService.Get();
            var imageContainers = mediaContainers.Where(mc => mc.MediaTypeId == 1 && mc.SourceId == ImageSource.Local && mc.IsEnabled).ToList();
            foreach (var imageContainer in imageContainers)
            {
                var searchLevel = imageContainer.UseSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                var files = Directory.GetFiles(imageContainer.Path, "*.*", searchLevel).ToList();
                foreach (var file in files)
                {
                    if (File.Exists(file))
                    {
                        _imageMetaDataRepository.Create(
                            new ImageMetaData
                            {
                                MediaContainerId = imageContainer.Id,
                                ItemName = Path.GetFileNameWithoutExtension(file),
                                FullFileName = file,
                                SourceId = imageContainer.SourceId,
                                GenreId = imageContainer.GenreId,
                            });
                    }
                }
            }
        }

        private IEnumerable<string> GetAvailableBlogFiles()
        {
            var checkListFile = _pathsAccessor.GetSystemImages()
                + Path.DirectorySeparatorChar + "URLFileCheckList.cld";
            var urlFileDir = _pathsAccessor.GetSystemImages()
                + Path.DirectorySeparatorChar + "URL Files";

            var returnValue = new List<string>();
            using (var fs = new FileStream(checkListFile, FileMode.Open))
            using (var binRead = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                {
                    var fileName = binRead.ReadString();
                    var isEnabled = binRead.ReadBoolean();
                    var fullFilePath = urlFileDir
                        + Path.DirectorySeparatorChar + fileName + ".txt";

                    if (File.Exists(fullFilePath) && isEnabled)
                        returnValue.Add(fullFilePath);
                }
                return returnValue.Distinct().ToList();
            }
        }

        private List<string> GetFiles(ImageContainer container, SearchOption searchLevel)
        {
            if (!container.IsEnabled)
                return new List<string>();
            var applicationConfig = _configurationAccessor.GetApplicationConfiguration();
            if (container.Genre == ImageGenre.Liked || container.Genre == ImageGenre.Disliked)
                return File.ReadAllLines(container.Path)
                    .Where(f => f.StartsWith("http") && container.Source == ImageSource.Remote)
                    .ToList();

            if (container.Source == ImageSource.Local)
            {
                var folderPath = container.Path;
                return Directory.GetFiles(folderPath, "*.*", searchLevel).ToList();
            }

            var urlFileDir = _pathsAccessor.GetSystemImages()
                + Path.DirectorySeparatorChar + "URL Files"
                + Path.DirectorySeparatorChar + container.Path;
            return File.ReadAllLines(urlFileDir).ToList();
        }

        public void Create(List<ImageMetaData> images)
        {
            _imageMetaDataRepository.Create(images);
        }

        private readonly IConfigurationAccessor _configurationAccessor;
        private readonly IPathsAccessor _pathsAccessor;
        private readonly IImageMetaDataRepository _imageMetaDataRepository;
        private readonly IMediaContainerService _mediaContainerService;
    }
}
