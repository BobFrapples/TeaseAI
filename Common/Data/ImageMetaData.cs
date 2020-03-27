using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// metadata about an image
    /// </summary>
    [Table("ImageMetaData")]
    public class ImageMetaData
    {
        /// <summary>
        /// Primary key of this in the database
        /// </summary>
        public int Id { get; set; }

        public int MediaContainerId { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public string ItemName { get; set; }

        public string FullFileName { get; set; }



        /// <summary>
        /// Create a new instances of this image meta data
        /// </summary>
        /// <returns></returns>
        public ImageMetaData Clone()
        {
            return new ImageMetaData
            {
                SourceId = SourceId,
                GenreId = GenreId,
                ItemName = ItemName,
                //ItemTags = ItemTags.ToList()
            };
        }

        [Obsolete("Use MediaContainer.SourceId")]
        public ImageSource SourceId { get; set; }

        [Obsolete("Use MediaContainer.GenreId")]
        public ImageGenre GenreId { get; set; }

    }
}
