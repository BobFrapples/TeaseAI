using System.ComponentModel.DataAnnotations.Schema;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// Tags for videos, images, etc.
    /// </summary>
    [Table("ItemTag")]
    public class ItemTag
    {
        /// <summary>
        /// Database key for this tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of this tag, Face, Furniture, etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this tag
        /// </summary>
        public string Description { get; set; }
    }
}
