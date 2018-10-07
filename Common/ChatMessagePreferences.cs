namespace TeaseAI.Common
{
    /// <summary>
    /// preferences on how chat messagaes should display (timestamp, color, etc)
    /// </summary>
    public class ChatMessagePreferences
    {
        public string BackgroundColor { get; set; }
        public string FontColor { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public string SenderColor { get; set; }
        public bool ShowSenderName { get; set; }
        public bool ShowTimeStamp { get; set; }
    }
}
