using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaseAI.Services.CommandDetection;

namespace TeaseAI.ServicesTests.CommandDetection
{
    public class VideoDetection_Tests
    {
        VideoDetection _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new VideoDetection();
        }
    }
}
