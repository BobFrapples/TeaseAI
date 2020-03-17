using System;
using System.Collections.Generic;
using System.Text;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Services.Accessors
{
    public class VideoAccessor : IVideoAccessor
    {
        private IConfigurationAccessor _configurationAccessor;

        public VideoAccessor(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<List<VideoMetaData>> GetVideoData(VideoGenre? Genre)
        {
            throw new NotImplementedException();
        }
    }
}
