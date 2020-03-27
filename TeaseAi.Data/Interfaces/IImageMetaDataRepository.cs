using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;

namespace TeaseAI.Data.Interfaces
{
    public interface IImageMetaDataRepository : IRepositoryBase<ImageMetaData>
    {
        List<ImageMetaData> Get(ImageSource? source, ImageGenre? genre);

        Result Create(IEnumerable<ImageMetaData> images);
        Result Update(IEnumerable<ImageMetaData> imageMetaDatas);
    }
}
