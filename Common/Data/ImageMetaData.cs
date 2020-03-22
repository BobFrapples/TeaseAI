using System;
using System.Collections.Generic;
using System.Linq;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// metadata about an image
    /// </summary>
    public class ImageMetaData
    {
        /// <summary>
        /// Is this local or blog
        /// </summary>
        public ImageSource Source { get; set; }

        /// <summary>
        /// what kind of image is it
        /// </summary>
        public ImageGenre Genre { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Tags for the item. Will never be null
        /// </summary>
        public List<ItemTag> ItemTags
        {
            get { return _itemTags ?? (_itemTags = new List<ItemTag>()); }
            set { _itemTags = value; }
        }

        private List<ItemTag> _itemTags;

        /// <summary>
        /// Create a new instances of this image meta data
        /// </summary>
        /// <returns></returns>
        public ImageMetaData Clone()
        {
            return new ImageMetaData
            {
                Source = Source,
                Genre = Genre,
                ItemName = ItemName,
                ItemTags = ItemTags.ToList()
            };
        }
    }
}
