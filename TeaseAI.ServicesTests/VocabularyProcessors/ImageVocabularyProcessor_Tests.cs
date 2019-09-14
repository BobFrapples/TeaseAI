using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services;
using TeaseAI.Services.VocabularyProcessors;

namespace TeaseAI.ServicesTests.VocabularyProcessors
{
    [TestClass]
    public class ImageVocabularyProcessor_Tests
    {
        ImageVocabularyProcessor _service;
        Mock<IImageAccessor> _imageAccessor;

        [TestInitialize]
        public void Initialize()
        {
            _imageAccessor = new Mock<IImageAccessor>();
            _imageAccessor.SetReturnsDefault(Result.Ok(new List<ImageMetaData>() { new ImageMetaData() }));
            _service = new ImageVocabularyProcessor(_imageAccessor.Object);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldSetImageCountCorrectly()
        {
            var keys = new List<string>
            {
                "#LocalImageCount",
                "#BlogImageCount",
                "#ButtImageCount",
                "#ButtsImageCount",
                "#BoobImageCount",
                "#BoobsImageCount",
                "#HardCoreImageCount",
                "#LesbianImageCount",
                "#BlowjobImageCount",
                "#FemdomImageCount",
                "#LezdomImageCount",
                "#HentaiImageCount",
                "#GayImageCount",
                "#MaledomImageCount",
                "#CaptionsImageCount",
                "#LikedImageCount",
                "#DislikedImageCount",
            };
            foreach (var key in keys)
            {
                var session = new Session(new DommePersonality(), new SubPersonality());
                var actual = _service.ReplaceVocabulary(session, key.Replace("#", "") + " count " + key);

                Assert.AreEqual(key.Replace("#", "") + " count 1", actual, key);
            }
        }
    }
}
