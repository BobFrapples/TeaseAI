using System.ComponentModel.DataAnnotations.Schema;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    /// <summary>
    /// Data model for media containers 
    /// where does the media live, is it remote or local, Video or Image
    /// </summary>
    [Table("MediaContainer")]
    public class MediaContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageGenre GenreId { get; set; }
        public string Path { get; set; }
        public bool UseSubFolders { get; set; }
        public ImageSource SourceId { get; set; }
        public bool IsEnabled { get; set; }
        public int MediaTypeId { get; set; }
    }
}
