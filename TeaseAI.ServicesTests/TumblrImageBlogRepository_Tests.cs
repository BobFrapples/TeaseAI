using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class TumblrImageBlogRepository_Tests
    {
        private TumblrImageBlogRepository _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new TumblrImageBlogRepository();
        }

        [TestCategory("IntegrationTest")]
        [TestMethod]
        public async Task Tumblr()
        {
            var data = await _service.GetBlogImagesAsync(new Uri("https://dog-lovers--club.tumblr.com/"), 0, 10);

            Assert.IsTrue(data.IsSuccess);
            Assert.IsTrue(data.Value.Count > 0);
        }
    }
}
