namespace TeaseAI.Common
{
    /// <summary>
    /// Domme's reply object to the sub speaking
    /// </summary>
    public class MessageProcessedResult
    {
        /// <summary>
        /// The new state of the session after processing
        /// </summary>
        public Session Session { get; set; }
        /// <summary>
        /// any response the Domme is making to the message
        /// </summary>
        public string MessageBack
        {
            get { return _messageBack ?? string.Empty; }
            set { _messageBack = value; }
        }
        private string _messageBack;
    }
}
