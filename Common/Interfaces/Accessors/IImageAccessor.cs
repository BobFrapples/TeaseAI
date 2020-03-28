using System.Collections.Generic;
using System.Threading.Tasks;
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
        List<ImageMetaData> Get(ImageSource? source, ImageGenre? genre);

        /// <summary>
        /// Save the ImageMetaData
        /// </summary>
        /// <param name="imageMetaDatas"></param>
        /// <returns></returns>
        Result Update(IEnumerable<ImageMetaData> imageMetaDatas);

        /// <summary>
        /// Initialize the database with any default data.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Create all images in  <paramref name="images"/>
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        void Create(List<ImageMetaData> images);

        Result<List<ImageMetaData>> GetImagesInContainer(int containerId);
        List<ImageMetaData> GetImagesWithTag(ItemTagId itemTagId);
    }
}
