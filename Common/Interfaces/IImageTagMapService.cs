using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IImageTagMapService
    {
        Result SetTagsForImage(int id, IEnumerable<int> tagIds);
        List<ImageTagMap> GetTagMapsForImage(int id);
        Result<ImageTagMap> Create(int id, ItemTagId imageTagId);
        Result Delete(int id, ItemTagId imageTagId);
    }
}
