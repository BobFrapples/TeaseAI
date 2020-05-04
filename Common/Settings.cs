using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    /// <summary>
    /// Various user configuration information
    /// </summary>
    public class Settings
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Settings()
        {
            Version = 1;
        }

        /// <summary>
        /// used to manage serialization. Changes when the model breaks compatibility
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Does the domme decide the range for <see cref="AllowsOrgasms"/>, or is it set custom
        /// </summary>
        public bool DoesDommeDecideOrgasmRange { get; set; }

        /// <summary>
        /// Custom allow often percentage
        /// </summary>
        public int AllowOrgasmOftenPercent { get; set; }

        /// <summary>
        /// Custom allow sometimes percentage
        /// </summary>
        public int AllowOrgasmSometimesPercent { get; set; }

        /// <summary>
        /// Custom allow rarely percentage
        /// </summary>
        public int AllowOrgasmRarelyPercent { get; set; }

        /// <summary>
        /// Does the domme decide the range for <see cref="RuinsOrgasms"/>, or is it set custom
        /// </summary>
        public bool DoesDommeDecideRuinRange { get; set; }

        /// <summary>
        /// Custom allow often percentage
        /// </summary>
        public int RuinOrgasmOftenPercent { get; set; }

        /// <summary>
        /// Custom allow sometimes percentage
        /// </summary>
        public int RuinOrgasmSometimesPercent { get; set; }

        /// <summary>
        /// Custom allow rarely percentage
        /// </summary>
        public int RuinOrgasmRarelyPercent { get; set; }

        /// <summary>
        /// Name of the domme personality.
        /// </summary>
        public string DommePersonality { get; set; }

        /// <summary>
        /// User Interface configuration
        /// </summary>
        public ChatSettings Chat
        {
            get { return _ui ?? (_ui = new ChatSettings()); }
            set { _ui = value; }
        }

        private ChatSettings _ui;
    }
}
