using System.Collections.Generic;
using System.Linq;
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

        public Result<ImageTagMap> Create(int itemId, ItemTagId itemTagId)
        {
            return _imageTagMapRepository.Create(new ImageTagMap
            {
                ImageId = itemId,
                ItemTagId = itemTagId,
            });

        }

        public Result Delete(int itemId, ItemTagId imageTagId)
        {
            var itemTagMap = _imageTagMapRepository.GetTagMapsForImage(itemId).FirstOrDefault(itm => itm.ItemTagId == imageTagId);

            if (itemTagMap == null)
                return Result.Ok();
            return _imageTagMapRepository.Delete(itemTagMap);
        }

        private IImageTagMapRepository _imageTagMapRepository;
    }
}
