﻿using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IMediaContainerService
    {
        void Initialize();
        void Update(List<MediaContainer> mediaContainers);
        List<MediaContainer> Get();
        Result<MediaContainer> Get(int containerId);
    }
}
