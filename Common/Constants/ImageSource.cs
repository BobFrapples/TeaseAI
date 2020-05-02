namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Where are this container's files stored
    /// </summary>
    public enum ImageSource
    {
        /// <summary>
        /// Files stored locally
        /// </summary>
        Local = 1,
        /// <summary>
        /// Files stored remotely (tumblr blogs, etc)
        /// </summary>
        Remote = 2,
        /// <summary>
        /// This container can be either
        /// </summary>
        Virtual = 3,
    }
}
