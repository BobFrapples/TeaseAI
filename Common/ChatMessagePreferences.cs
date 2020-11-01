namespace TeaseAI.Common
{
    /// <summary>
    /// preferences on how chat messagaes should display (timestamp, color, etc)
    /// </summary>
    public class ChatMessagePreferences
    {
        /// <summary>
        /// Background color
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SenderColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowSenderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowTimeStamp { get; set; }
    }
}
