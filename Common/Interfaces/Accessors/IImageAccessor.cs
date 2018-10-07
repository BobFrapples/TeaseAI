using System.Collections.Generic;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces.Accessors
{
    public interface IImageAccessor
    {
        Result<List<ImageMetaData>> GetImageMetaDataList(ImageSource? source, ImageGenre? genre);
    }
}
