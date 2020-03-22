using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class ImageAccessor : IImageAccessor
    {
        public ImageAccessor(IConfigurationAccessor configurationAccessor
            , IPathsAccessor pathsAccessor)
        {
            _configurationAccessor = configurationAccessor;
            _pathsAccessor = pathsAccessor;
        }

        public Result<List<ImageMetaData>> GetImageMetaDataList(ImageSource? source, ImageGenre? genre)
        {
            var applicationConfig = _configurationAccessor.GetApplicationConfiguration();
            var returnValue = new List<ImageMetaData>();

            foreach (var container in applicationConfig.ImageContainers)
            {
                if ((genre.GetValueOrDefault(container.Genre) != container.Genre)
                    || source.GetValueOrDefault(container.Source) != container.Source)
                    continue;

                var searchLevel = container.UseSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                if (container.Source == ImageSource.Remote && container.Genre == ImageGenre.Blog)
                {
                    foreach (var blogFile in GetAvailableBlogFiles())
                    {
                        container.Path = Path.GetFileName(blogFile);
                        var files = GetFiles(container, searchLevel);
                        foreach (var file in files)
                        {
                            returnValue.Add(new ImageMetaData
                            {
                                Genre = container.Genre,
                                ItemName = file,
                                Source = container.Source,
                            });
                        }
                    }
                }
                else
                {
                    var files = GetFiles(container, searchLevel);
                    foreach (var file in files)
                    {
                        returnValue.Add(new ImageMetaData
                        {
                            Genre = container.Genre,
                            ItemName = file,
                            Source = container.Source,
                        });
                    }
                }
            }
            return Result.Ok(returnValue);
        }

        public Result SaveImageMetaData(List<ImageMetaData> imageMetaDatas)
        {
            throw new NotImplementedException();
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

        private readonly IConfigurationAccessor _configurationAccessor;
        private readonly IPathsAccessor _pathsAccessor;

        //private string _systemImageDir  = Application.StartupPath + "\Images\System\"
        //private string _pathUrlFileDir  = mySystemImageDIr + "URL Files\"
    }
}
