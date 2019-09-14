using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class VocabularyProcessor_Tests
    {
        VocabularyProcessor _service;
        Mock<IVocabularyAccessor> _vocabularyAccessor;
        Mock<IImageAccessor> _imageAccessor;
        Mock<RandomNumberService> _randomNumberService;

        [TestInitialize]
        public void Initialize()
        {
            _vocabularyAccessor = new Mock<IVocabularyAccessor>();
            _imageAccessor = new Mock<IImageAccessor>();
            _randomNumberService = new Mock<RandomNumberService>();
            _service = new VocabularyProcessor(new LineCollectionFilter(), new LineService(), _vocabularyAccessor.Object, _imageAccessor.Object, _randomNumberService.Object);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReplaceHashCorrectly()
        {
            var session = new Session(new DommePersonality(), new SubPersonality());
            var actual = _service.ReplaceVocabulary(session, "#");

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReplaceSubNameCorrectly()
        {
            var session = new Session(new DommePersonality(), new SubPersonality() { Name = "Sub" });
            var actual = _service.ReplaceVocabulary(session, "#SubName");

            Assert.AreEqual("Sub", actual);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReplaceSubNameCorrectly_WhenWithNonLetters()
        {
            var session = new Session(new DommePersonality(), new SubPersonality() { Name = "Sub" });
            var actual = _service.ReplaceVocabulary(session, "#SubName...");

            Assert.AreEqual("Sub...", actual);
        }

        [TestMethod]
        public void ReplaceVocabulary_ShouldReplaceCockCorrectly_WhenSubCallsCockAClit()
        {
            var session = new Session(new DommePersonality(), new SubPersonality() { CallCockAClit = true });
            _vocabularyAccessor.Setup(obj => obj.GetVocabulary(session.Domme, "#CockToClit"))
                .Returns(Result.Ok(new List<string> { "clit" }));
            var actual = _service.ReplaceVocabulary(session, "Rub your #Cock");

            Assert.AreEqual("Rub your clit", actual);
        }
    }
}
