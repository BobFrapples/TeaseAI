using System.ComponentModel.DataAnnotations.Schema;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    [Table("ImageTagMap")]
    public class ImageTagMap
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public ItemTagId ItemTagId { get; set; }
    }
}
