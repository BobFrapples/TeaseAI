using System.Diagnostics;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    [DebuggerDisplay("{Key} - {Genre}")]
    public class VideoMetaData
    {
        /// <summary>
        /// Does this video have the domme in it.
        /// </summary>
        public bool FeaturesDomme { get; set; }
        /// <summary>
        /// What type of video is this
        /// </summary>
        public VideoGenre Genre { get; set; }
        /// <summary>
        /// Key to identify the video
        /// </summary>
        public string Key { get; set; }
    }
}
