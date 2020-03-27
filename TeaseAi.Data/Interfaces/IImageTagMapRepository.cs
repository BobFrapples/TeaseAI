using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Data;

namespace TeaseAI.Data.Interfaces
{
    public interface IImageTagMapRepository : IRepositoryBase<ImageTagMap>
    {
        Result SetTagsForImage(int imageId, IEnumerable<int> tags);
        List<ImageTagMap> GetTagMapsForImage(int imageId);
    }
}
