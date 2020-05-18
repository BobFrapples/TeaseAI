using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.Accessors;

namespace TeaseAI.ServicesTests.Accessors
{
    [TestClass]
    public class SettingsAccessor_Tests
    {
        SettingsAccessor _settingsAccessor;
        Mock<IConfigurationAccessor> _configurationAccessor;

        [TestInitialize]
        public void Initialize()
        {
            _configurationAccessor = new Mock<IConfigurationAccessor>();
            _settingsAccessor = new SettingsAccessor(_configurationAccessor.Object);
        }

        [TestMethod]
        public void Serialize_ShouldSerialize_WithoutThrowingException()
        {
            var settings = _settingsAccessor.CreateDefaultSettings();

            var settingsJson = _settingsAccessor.Serialize(settings);

            Assert.IsFalse(string.IsNullOrWhiteSpace(settingsJson));
        }

        [TestMethod]
        public void Serialize_ShouldDeserialize_WithoutThrowingException()
        {
            var settingsJson = _settingsAccessor.Serialize(_settingsAccessor.CreateDefaultSettings());
            var settings = _settingsAccessor.Deserialize(settingsJson);

            Assert.IsNotNull(settings);
        }
    }
}
