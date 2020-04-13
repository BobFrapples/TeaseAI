using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IMediaContainerService
    {
        void Initialize();
        Result<MediaContainer> Create(MediaContainer mediaContainer);
        void Update(List<MediaContainer> mediaContainers);
        List<MediaContainer> Get();
        Result<MediaContainer> Get(int containerId);
    }
}
