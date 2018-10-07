using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaseAI.Common;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Services.CommandProcessor;
using TrueFakes;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using System.Collections.Generic;

namespace TeaseAI.ServicesTests.CommandProcessors
{
    [TestClass]
    public class ShowHardcoreImageCommandProcessor_Tests
    {
        ShowHardcoreImageCommandProcessor _service;
        IImageAccessor _imageAccessor;
        Session _session;

        [TestInitialize]
        public void Initialize()
        {
            _imageAccessor = TrueFake.Of<IImageAccessor>();
            _service = new ShowHardcoreImageCommandProcessor(_imageAccessor);
            _session = new Session(new DommePersonality(), new SubPersonality());
        }

        #region IsRelevant
        [TestMethod]
        public void IsRelevant_ShouldReturnTrue_WhenCommandInString()
        {
            Assert.IsTrue(_service.IsRelevant(_session, Keyword.ShowHardcoreImage));
            Assert.IsTrue(_service.IsRelevant(_session, "some text before the command" + Keyword.ShowHardcoreImage));
            Assert.IsTrue(_service.IsRelevant(_session, Keyword.ShowHardcoreImage + " some text after the command"));
            Assert.IsTrue(_service.IsRelevant(_session, "some text before the command" + Keyword.ShowHardcoreImage + " some text after the command"));
        }

        [TestMethod]
        public void IsRelevant_ShouldReturnFalse_WhenCommandNotInString()
        {
            Assert.IsFalse(_service.IsRelevant(_session, Keyword.ShowImage), "OtherCommand");
            Assert.IsFalse(_service.IsRelevant(_session, string.Empty), "empty");
            Assert.IsFalse(_service.IsRelevant(_session, null), "null");
        }
        #endregion

        #region DeleteCommandFrom
        [TestMethod]
        public void DeleteCommandFrom_ShouldRemoveCommand_WhenCommandInString()
        {
            Assert.AreEqual(string.Empty, _service.DeleteCommandFrom(Keyword.ShowHardcoreImage));
            Assert.AreEqual("before", _service.DeleteCommandFrom("before " + Keyword.ShowHardcoreImage));
            Assert.AreEqual("after", _service.DeleteCommandFrom(Keyword.ShowHardcoreImage + " after"));
            Assert.AreEqual("before  after", _service.DeleteCommandFrom("before " + Keyword.ShowHardcoreImage + " after"));
        }

        [TestMethod]
        public void DeleteCommandFrom_ShouldReturnSame_WhenCommandNotInString()
        {
            Assert.AreEqual(Keyword.ShowImage, _service.DeleteCommandFrom(Keyword.ShowImage));
            Assert.AreEqual(string.Empty, _service.DeleteCommandFrom(string.Empty));
            Assert.AreEqual(string.Empty, _service.DeleteCommandFrom(null));
        }
        #endregion

        #region PerformCommand
        [TestMethod]
        public void PerformCommand_ShouldFail_WhenAccessorFails()
        {
            Arrange.Call(() => _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Hardcore))
                .Returns(Result.Fail<List<ImageMetaData>>("Testing failure"));

            var actual = _service.PerformCommand(_session, Keyword.ShowHardcoreImage);
            Assert.AreEqual("Testing failure", actual.Error.Message);
        }

        [TestMethod]
        public void PerformCommand_ShouldFail_WhenAccessorFindsNoImages()
        {
            Arrange.Call(() => _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Hardcore))
                .Returns(Result.Ok(new List<ImageMetaData>()));

            var actual = _service.PerformCommand(_session, Keyword.ShowHardcoreImage);
            Assert.AreEqual(ErrorMessage.NoImagesFound, actual.Error.Message);
        }

        [TestMethod]
        public void PerformCommand_ShouldPassSameSession_WhenSucceeds()
        {
            var eventSession = default(Session);
            _service.CommandProcessed += (s, e) => eventSession = e.Session;

            Arrange.Call(() => _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Hardcore))
                .Returns(Result.Ok(new List<ImageMetaData>() { new ImageMetaData() }));

            var actual = _service.PerformCommand(_session, Keyword.ShowHardcoreImage);
            Assert.AreEqual(actual.Value, eventSession);
        }

        [TestMethod]
        public void PerformCommand_ShouldPassImageMetaData_WhenSucceeds()
        {
            var imageData = default(ImageMetaData);
            _service.CommandProcessed += (s, e) => imageData = e.Parameter as ImageMetaData;

            var foundImage = new ImageMetaData();
            Arrange.Call(() => _imageAccessor.GetImageMetaDataList(default(ImageSource?), ImageGenre.Hardcore))
                .Returns(Result.Ok(new List<ImageMetaData>() { foundImage }));

            var actual = _service.PerformCommand(_session, Keyword.ShowHardcoreImage);
            Assert.AreEqual(foundImage, imageData);
        }

        #endregion
    }
}
