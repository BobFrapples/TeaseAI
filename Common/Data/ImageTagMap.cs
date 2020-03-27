using System.ComponentModel.DataAnnotations.Schema;

namespace TeaseAI.Common.Data
{
    [Table("ImageTagMap")]
    public class ImageTagMap
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int ItemTagId { get; set; }
    }
}
