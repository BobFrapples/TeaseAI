using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Services.CommandProcessor;

namespace TeaseAI.ServicesTests.CommandProcessors
{
    [TestClass]
    public class IfCommandProcessor_Tests
    {
        IfCommandProcessor _service;
        Mock<IBookmarkService> _bookmarkService;

        [TestInitialize]
        public void Initialize()
        {
            _bookmarkService = new Mock<IBookmarkService>();
            _service = new IfCommandProcessor(new Services.LineService(), _bookmarkService.Object,null, null, null);
        }


        [TestMethod]
        public void ParseCommand_ParsesCorrectly()
        {
            _bookmarkService.Setup(obj => obj.FindBookmark(It.IsAny<IEnumerable<string>>(), "(no_edge)"))
                .Returns(Result.Ok(1));

            var lines = new List<string>
            {
                "@If[png__avoid_edge_counter]=[0]Then(no_edge)",
                "Wow, @If[png__avoid_edge_counter]=[0]Then(no_edge)"
            };

            foreach (var line in lines)
            {
                var script = new Script(new ScriptMetaData(), new List<string>());
                var actual = _service.ParseCommand(script, "personality", line);

                Assert.IsTrue(actual.IsSuccess, line);
            }
        }

        [TestMethod]
        public void ParseCommand_FailsToParseCorrectly()
        {
            _bookmarkService.Setup(obj => obj.FindBookmark(It.IsAny<IEnumerable<string>>(), "(no_edge)"))
                .Returns(Result.Fail<int>("Failure"));

            var lines = new List<string>
            {
                "@If[png__avoid_edge_counter]=[0]Then(no_edge)",
                "Wow, @If[png__avoid_edge_counter]=[0]Then(no_edge) @If[png__avoid_edge_counter]>[0]Then(edge) "
            };

            foreach (var line in lines)
            {
                var script = new Script(new ScriptMetaData(), new List<string>());
                var actual = _service.ParseCommand(script, "personality", line);

                Assert.IsTrue(actual.IsFailure, line);
                Assert.AreEqual("Failure", actual.Error.Message, line);
            }
        }
    }
}
