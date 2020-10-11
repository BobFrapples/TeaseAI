using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaseAI.Common;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class InterpolationProcessor_Tests
    {
         InterpolationProcessor _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new InterpolationProcessor();
        }

        [DataTestMethod]
        [DataRow("{Session.IsBeforeTease}", "True")]
        [DataRow("{Session.Domme.Name} thinks {Session.IsBeforeTease}", "Test Name thinks True")]
        [DataRow("{Session.Sub.Name}", "")]
        [DataRow("Sally sells sea shells", "Sally sells sea shells")]
        public void Interpolate_ShouldReplaceRequestedProperties(string line, string expected)
        {
            var session = new Session(new DommePersonality(), new SubPersonality());
            session.IsBeforeTease = true;
            session.Domme.Name = "Test Name";

            var actual = _service.Interpolate(session, line);

            Assert.IsTrue(actual.IsSuccess);
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void Interpolate_ShouldFail_WhenRequestedPropertyDoesNotExist()
        {

            var session = new Session(new DommePersonality(), new SubPersonality());

            var actual = _service.Interpolate(session, "{Session.NonExistant}");

            Assert.IsTrue(actual.IsFailure);
        }

        [TestMethod]
        public void Parse_ShouldFail_WhenRequestedPropertyIsObsolete()
        {

            var session = new Session(new DommePersonality(), new SubPersonality());

            var actual = _service.Parse(session, "{Session.IsBeforeTease}");

            Assert.IsTrue(actual.IsFailure);
        }

        [TestMethod]
        public void Parse_ShouldFail_WhenRequestedPropertyDoesNotExist()
        {

            var session = new Session(new DommePersonality(), new SubPersonality());

            var actual = _service.Parse(session, "{Session.NonExistant}");

            Assert.IsTrue(actual.IsFailure);
        }

        [DataTestMethod]
        [DataRow("{Session.Phase}")]
        [DataRow("{Session.Domme.Name}")]
        [DataRow("{Session.Domme.Name} thinks {Session.Phase}")]
        [DataRow("Sally sells sea shells")]
        public void Parse_ShouldSucceed_WhenEverythingIsRight(string line)
        {

            var session = new Session(new DommePersonality(), new SubPersonality());

            var actual = _service.Parse(session, line);

            Assert.IsTrue(actual.IsSuccess);
        }
    }
}
