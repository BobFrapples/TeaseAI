using System.Collections.Generic;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Data;

namespace TeaseAI.Data.Interfaces
{
    public interface IItemTagRepository: IRepositoryBase<ItemTag>
    {
        List<ItemTag> GetTagsForImage(int imageId);
    }
}
