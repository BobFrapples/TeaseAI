using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services;
using TeaseAI.Services.Accessors;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class InterpolationProcessor_Tests
    {
        InterpolationProcessor _service;
        Mock<ISettingsAccessor> _settingsAccessor;
        [TestInitialize]
        public void Initialize()
        {
            _settingsAccessor = new Mock<ISettingsAccessor>();
            _service = new InterpolationProcessor(_settingsAccessor.Object);
        }

        [DataTestMethod]
        [DataRow("{Session.IsBeforeTease}", "True")]
        [DataRow("{Session.Domme.Name} thinks {Session.IsBeforeTease}", "Test Name thinks True")]
        [DataRow("{Session.Sub.Name}", "Test Sub")]
        [DataRow("{Session.Domme.Name},{Session.Sub.Name}", "Test Name,Test Sub")]
        [DataRow("Sally sells sea shells", "Sally sells sea shells")]
        public void Interpolate_ShouldReplaceRequestedProperties(string line, string expected)
        {
            var session = new Session(new DommePersonality(), new SubPersonality());
            session.Phase = SessionPhase.BeforeSession;
            session.Domme.Name = "Test Name";
            session.Sub.Name = "Test Sub";

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
