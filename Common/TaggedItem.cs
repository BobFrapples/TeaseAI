using System.Collections.Generic;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// An item and its associated Tags
    /// </summary>
    public class TaggedItem
    {
        /// <summary>
        /// Name of the item
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Tags for the item. Will never be null
        /// </summary>
        public List<Constants.TaggedItem> ItemTags
        {
            get { return _itemTags ?? (_itemTags = new List<Constants.TaggedItem>()); }
            set { _itemTags = value; }
        }
        private List<Constants.TaggedItem> _itemTags;
    }
}
