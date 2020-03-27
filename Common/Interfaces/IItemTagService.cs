using System.Collections.Generic;
using System.Threading.Tasks;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IItemTagService
    {
        void Initialize();
        List<ItemTag> Get();
    }
}
