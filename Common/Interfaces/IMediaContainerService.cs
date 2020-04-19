using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IMediaContainerService
    {
        Result<MediaContainer> Create(MediaContainer mediaContainer);
        Result<List<MediaContainer>> Create(List<MediaContainer> mediaContainer);

        Result<List<MediaContainer>> Update(List<MediaContainer> mediaContainers);

        /// <summary>
        /// Get all media containers
        /// </summary>
        /// <returns></returns>
        List<MediaContainer> Get();

        /// <summary>
        /// Get a specific media container by <paramref name="containerId"/>
        /// </summary>
        /// <param name="containerId"></param>
        /// <returns></returns>
        Result<MediaContainer> Get(int containerId);

        /// <summary>
        /// Get all containers for <paramref name="mediaTypeId"/> sourced at <paramref name="imageSource"/>
        /// </summary>
        /// <param name="mediaTypeId"></param>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        List<MediaContainer> Get(int mediaTypeId, ImageSource imageSource);
    }
}
