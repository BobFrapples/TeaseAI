using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Services.Services
{
    public class ImageTagMapService : IImageTagMapService
    {
        public ImageTagMapService(IImageTagMapRepository imageTagMapRepository)
        {
            _imageTagMapRepository = imageTagMapRepository;
        }

        public Result SetTagsForImage(int imageId, IEnumerable<int> tagIds)
        {
            return _imageTagMapRepository.SetTagsForImage(imageId, tagIds);
        }

        public List<ImageTagMap> GetTagMapsForImage(int imageId)
        {
            return _imageTagMapRepository.GetTagMapsForImage(imageId);
        }

        private IImageTagMapRepository _imageTagMapRepository;
    }
}
