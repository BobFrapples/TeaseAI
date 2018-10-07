using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TeaseAI.Services;

namespace TeaseAI.ServicesTests
{
    [TestClass]
    public class LineService_Tests
    {
        LineService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new LineService();
        }

        [TestMethod]
        public void GetData_ShouldReturnData()
        {
            var actual = _service.GetParenData("@Cup(A)", "@Cup(");

            Assert.AreEqual("A", actual.Value.Single());
        }

        [TestMethod]
        public void GetData_ShouldReturnTwoRecords_WhenExpected()
        {
            var actual = _service.GetParenData("@Cup(A,C)", "@Cup(");

            Assert.AreEqual(2, actual.Value.Count);
            Assert.IsTrue( actual.Value.Contains("A"));
            Assert.IsTrue( actual.Value.Contains("C"));
        }

        [TestMethod]
        public void GetData_ShouldReturnFirstCommand_WhenMultipleMatches()
        {
            var actual = _service.GetParenData("This is a test of my @Cup(A,C) @Cup(D,E) boobs", "@Cup(");

            Assert.AreEqual(2, actual.Value.Count);
            Assert.IsTrue(actual.Value.Contains("A"));
            Assert.IsTrue(actual.Value.Contains("C"));
        }
    }
}
