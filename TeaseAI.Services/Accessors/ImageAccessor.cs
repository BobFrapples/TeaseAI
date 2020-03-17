using System;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class ImageAccessor : IImageAccessor
    {
        private IConfigurationAccessor _configurationAccessor;

        public ImageAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<List<ImageMetaData>> GetImageMetaDataList(ImageSource? source, ImageGenre? genre)
        {
            throw new NotImplementedException();
        }
    }
}
