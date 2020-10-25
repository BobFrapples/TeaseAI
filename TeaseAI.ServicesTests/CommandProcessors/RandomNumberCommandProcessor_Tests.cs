using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Services.CommandProcessor;

namespace TeaseAI.ServicesTests.CommandProcessors
{
    [TestClass]
    public class RandomNumberCommandProcessor_Tests
    {
        RandomNumberCommandProcessor _service;
        Mock<IRandomNumberService> _randomNumberService;

        [TestInitialize]
        public void TestInitialize()
        {
            _randomNumberService = new Mock<IRandomNumberService>();
            _service = new RandomNumberCommandProcessor(new Services.LineService(), _randomNumberService.Object);
        }

        [DataTestMethod]
        [DataRow(Keyword.RandomNumber + "100)", true)]
        [DataRow(Keyword.RandomNumber + "0, 100)", true)]
        [DataRow(Keyword.RandomNumber + "{interpolation})", true)]
        [DataRow(Keyword.RandomNumber + "{interpolation}, 39)", true)]
        [DataRow(Keyword.RandomNumber + "-1,{interpolation})", true)]
        [DataRow(Keyword.RandomNumber + "{ipol},{interpolation})", true)]
        [DataRow(Keyword.RandomNumber + "ipol,{interpolation})", false)]
        [DataRow(Keyword.RandomNumber + "0,3,4)", false)]
        [DataRow(Keyword.RandomNumber + "0,text)", false)]
        public void ParseCommand_ShouldSucceed(string line, bool expected)
        {
            var script = new Script(new ScriptMetaData(), new List<string>());
            var actual = _service.ParseCommand(script, "mock-personality", line);

            Assert.AreEqual(expected, actual.IsSuccess);

        }
    }
}
