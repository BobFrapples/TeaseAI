using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces;
using TeaseAI.Services.VocabularyProcessors;

namespace TeaseAI.ServicesTests.VocabularyProcessors
{
    [TestClass]
    public class RandomVocabularyProcessor_Tests
    {
        RandomVocabularyProcessor _service;
        Mock<IRandomNumberService> _randomNumberService;

        [TestInitialize]
        public void Initialize()
        {
            _randomNumberService = new Mock<IRandomNumberService>();
            _randomNumberService.SetReturnsDefault(10);
            _service = new RandomVocabularyProcessor(new Services.LineService(), _randomNumberService.Object);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldSetImageCountCorrectly()
        {
            var session = new Session(new DommePersonality(), new SubPersonality());
            var actual = _service.ReplaceVocabulary(session, "Your lucky number is #Random(10,10)");

            Assert.AreEqual("Your lucky number is 10", actual);
        }
    }
}
