using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IMediaContainerService
    {
        void Initialize();
        List<MediaContainer> Get();
        void Update(List<MediaContainer> mediaContainers);
    }
}
