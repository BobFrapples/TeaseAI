using System;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Events
{
    /// <summary>
    /// Tell the UI to start playing a video
    /// </summary>
    public class PlayVideoEventArgs : EventArgs
    {
        public VideoMetaData VideoMetaData { get; set; }
        /// <summary>
        /// Should the player randomize the starting location
        /// </summary>
        public bool ShouldRandomizeStart { get; set; }
        /// <summary>
        /// Did the video start, or did it fail, and why
        /// </summary>
        public Result Result { get; set; }
    }
}
