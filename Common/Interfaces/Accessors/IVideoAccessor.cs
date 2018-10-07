using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IVideoAccessor
    {
        /// <summary>
        /// Get *ALL* video meta data. including domme, joi, and ch
        /// </summary>
        /// <param name="Genre"></param>
        /// <returns></returns>
        Result<List<VideoMetaData>> GetVideoData(VideoGenre? Genre);
    }
}
