using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IImageAccessor
    {
        /// <summary>
        /// Get the ImageMetaData requested
        /// </summary>
        /// <param name="source"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        Result<List<ImageMetaData>> GetImageMetaDataList(ImageSource? source, ImageGenre? genre);

        /// <summary>
        /// Save the ImageMetaData
        /// </summary>
        /// <param name="imageMetaDatas"></param>
        /// <returns></returns>
        Result SaveImageMetaData(List<ImageMetaData> imageMetaDatas);
    }
}
