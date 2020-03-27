using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IImageTagMapService
    {
        Result SetTagsForImage(int id, IEnumerable<int> tagIds);
        List<ImageTagMap> GetTagMapsForImage(int id);
    }
}
