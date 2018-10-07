using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TeaseAI.Common.Constants;
using TeaseAI.Services.TagData;

namespace TeaseAI.ServicesTests.TagData
{
    [TestClass]
    public class ParseOldTagDataService_Tests
    {
        ParseOldTagDataService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new ParseOldTagDataService();
        }

        [TestMethod]
        public void ParseTagData_ShouldFail_WhenDataNull()
        {
            var actual = _service.ParseTagData(null);

            Assert.IsTrue(actual.IsFailure);
        }

        [TestMethod]
        public void ParseTagData_ShouldParse_WhenOneLine()
        {
            var data = "11659355_10153451530162269_1602366008473968760_n.jpg TagFace";
            var actual = _service.ParseTagData(data);

            Assert.IsTrue(actual.IsSuccess);

            var taggedItem = actual.Value.Single();
            Assert.AreEqual("11659355_10153451530162269_1602366008473968760_n.jpg", taggedItem.ItemName);
            Assert.AreEqual(ItemTag.Face, taggedItem.ItemTags.Single());
        }

        [TestMethod]
        public void ParseTagData_ShouldParse_WhenSpaceInFileName()
        {
            var data = "princess face.jpg TagFace TagNaked";
            var actual = _service.ParseTagData(data);

            Assert.IsTrue(actual.IsSuccess);

            var taggedItem = actual.Value.Single();
            Assert.AreEqual("princess face.jpg", taggedItem.ItemName);
        }

        [TestMethod]
        public void ParseTagData_ShouldParse_WhenMultipleLines()
        {
            var data = "princess face.jpg TagFace TagNaked" + Environment.NewLine 
                + "11659355_10153451530162269_1602366008473968760_n.jpg TagFace";

            var actual = _service.ParseTagData(data);

            Assert.IsTrue(actual.IsSuccess);

            Assert.AreEqual("princess face.jpg", actual.Value[0].ItemName);
            Assert.AreEqual("11659355_10153451530162269_1602366008473968760_n.jpg", actual.Value[1].ItemName);

        }

    }
}
