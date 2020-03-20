using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services;
using TeaseAI.Services.CommandProcessor;

namespace TeaseAI.ServicesTests.CommandProcessors
{
    [TestClass]
    public class SetVarCommandProcessor_Tests
    {
        ICommandProcessor _commandProcessor;
        Mock<IVariableAccessor> _variableAccessor;

        [TestInitialize]
        public void Initialize()
        {
            _variableAccessor = new Mock<IVariableAccessor>();
            _commandProcessor = new SetVarCommandProcessor(new LineService(), _variableAccessor.Object);
        }

        [TestMethod]
        public void ParseCommand_ShouldDetectUnparsable()
        {
            var actual = _commandProcessor.ParseCommand(null, null, "@NullResponse @SetVar[pthevBegForCEI] =[0");

            Assert.IsTrue(actual.IsFailure);
        }

        [TestMethod]
        public void ParseCommand_ShouldParseCorrectly()
        {
            var actual = _commandProcessor.ParseCommand(null, null, "@NullResponse @SetVar[pthevBegForCEI] =[0]");

            Assert.IsTrue(actual.IsSuccess);
        }
    }
}
