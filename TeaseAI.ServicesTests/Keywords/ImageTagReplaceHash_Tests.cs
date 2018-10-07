using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Services.Keywords;

namespace TeaseAI.ServicesTests.Keywords
{
    [TestClass]
    public class ImageTagReplaceHash_Tests
    {
        ImageTagReplaceHash _service;

        [TestInitialize]
        public void Initialize()
        {
            // this is a sample tagline with all variable tags set
            //var tagLine = "20180603_190605_Burst01.jpg TagFace TagBoobs TagHalfDressed TagCloseUp TagGlaring TagGarmentBra TagUnderwearblack-lace TagTattooghost TagSexToydildo TagFurniturebed";

            _service = new ImageTagReplaceHash();
        }

        [TestMethod]
        public void ReplaceImageTags_ShouldReplaceGarmentHash_WhenNoGarmentSet()
        {
            var taggedItem = new TaggedItem()
            {
                ItemName = "item_name.jpg",
                ItemTags = new List<ItemTag>()
                {
                    ItemTag.GarmentCovering,
                }
            };

            var actual = _service.ReplaceImageTags("my #TagGarment is black", taggedItem);

            Assert.AreEqual("my garment is black", actual);
        }

        [TestMethod]
        public void ReplaceImageTags_ShouldReplaceGarmentHash_WhenNoGarmentTag()
        {
            var taggedItem = new TaggedItem()
            {
                ItemName = "item_name.jpg",
            };

            var actual = _service.ReplaceImageTags("my #TagGarment is black", taggedItem);

            Assert.AreEqual("my garment is black", actual);
        }

        [TestMethod]
        public void ReplaceImageTags_ShouldReplaceGarmentHash_WhenGarmentTagSet()
        {
            var taggedItem = new TaggedItem()
            {
                ItemName = "item_name.jpg",
                ItemTags = new List<ItemTag>() { (ItemTag)"TagGarmentBra" },
            };

            var actual = _service.ReplaceImageTags("my #TagGarment is black", taggedItem);

            Assert.AreEqual("my bra is black", actual);
        }
    }
}
