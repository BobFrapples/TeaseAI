using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaseAI.Services.CommandDetection;

namespace TeaseAI.ServicesTests.CommandDetection
{
    [TestClass]
    public class TagDetection_Tests
    {
        TagDetection _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new TagDetection();
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagFaceCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagFace", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagFaceCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagFace", "TagFace"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagBoobsCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagBoobs", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagBoobsCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagBoobs", "TagBoobs"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagPussyCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagPussy", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagPussyCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagPussy", "TagPussy"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagAssCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagAss", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagAssCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagAss", "TagAss"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagFeetCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagFeet", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagFeetCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagFeet", "TagFeet"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagLegsCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagLegs", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagLegsCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagLegs", "TagLegs"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagMasturbatingCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagMasturbating", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagMasturbatingCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagMasturbating", "TagMasturbating"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagSuckingCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagSucking", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagSuckingCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagSucking", "TagSucking"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagFullyDressedCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagFullyDressed", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagFullyDressedCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagFullyDressed", "TagFullyDressed"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagHalfDressedCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagHalfDressed", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagHalfDressedCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagHalfDressed", "TagHalfDressed"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagNakedCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagNaked", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagNakedCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagNaked", "TagNaked"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagSideViewCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagSideView", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagSideViewCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagSideView", "TagSideView"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagGarmentCoveringCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagGarmentCovering", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagGarmentCoveringCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagGarmentCovering", "TagGarmentCovering"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagHandsCoveringCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagHandsCovering", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagHandsCoveringCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagHandsCovering", "TagHandsCovering"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagCloseUpCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagCloseUp", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagCloseUpCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagCloseUp", "TagCloseUp"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagPiercingCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagPiercing", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagPiercingCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagPiercing", "TagPiercing"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagSmilingCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagSmiling", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagSmilingCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagSmiling", "TagSmiling"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagUnderwearCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagUnderwear", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagUnderwearCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagUnderwear", "TagUnderwear"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagTattooCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagTattoo", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagTattooCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagTattoo", "TagTattoo"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagSexToyCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagSexToy", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagSexToyCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagSexToy", "TagSexToy"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenTagFurnitureCommandWithNoTag()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@TagFurniture", "foobar"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenTagFurnitureCommandWithTag()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@TagFurniture", "TagFurniture"));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenNoCommand()
        {
            Assert.IsTrue(_service.ShouldKeepLine("No command here", "TagFurniture"));
        }
    }
}
