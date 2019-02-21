using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaseAI.Common;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class VocabularyProcessor_Tests
    {
        VocabularyProcessor _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new VocabularyProcessor(new LineCollectionFilter());
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
    }
}
