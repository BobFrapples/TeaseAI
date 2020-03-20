using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services;
using TeaseAI.Services.CommandProcessor;

namespace TeaseAI.ServicesTests.CommandProcessors
{
    [TestClass]
    public class CheckFlagCommandProcessor_Tests
    {
        private ICommandProcessor _commandProcessor;
        private Mock<IBookmarkService> _bookmarkService;
        private Mock<IFlagAccessor> _flagAccessor;

        [TestInitialize]
        public void Initialize()
        {
            _bookmarkService = new Mock<IBookmarkService>();
            _flagAccessor = new Mock<IFlagAccessor>();
            _commandProcessor = new CheckFlagCommandProcessor(_flagAccessor.Object, new LineService(), _bookmarkService.Object);
        }

        [TestMethod]
        public void ParseCommand_ShouldSucceed_WhenBookmarkExists()
        {
            var script = new Script(new ScriptMetaData(),
                new List<string>
                {
                    "@CheckFlag(pvCheckOrgasmChance)",
                    "Empty line.",
                    "(pvCheckOrgasmChance)"
                });

            _bookmarkService.Setup(obj => obj.FindBookmark(script.Lines, "pvCheckOrgasmChance"))
                .Returns(Result.Ok(2));

            var actual = _commandProcessor.ParseCommand(script, string.Empty, "@CheckFlag(pvCheckOrgasmChance)");

            Assert.IsTrue(actual.IsSuccess);
        }
    }
}
