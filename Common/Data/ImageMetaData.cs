using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    public class ImageMetaData
    {
        public ImageSource Source { get; set; }
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
    }
}
