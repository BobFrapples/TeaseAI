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

        public int AllowOrgasmOftenPercent { get; set; }
        public int AllowOrgasmSometimesPercent { get; set; }
        public int AllowOrgasmRarelyPercent { get; set; }

        /// <summary>
        /// Does the domme decide the range for <see cref="RuinsOrgasms"/>, or is it set custom
        /// </summary>
        public bool DoesDommeDecideRuinRange { get; set; }

        public int RuinOrgasmOftenPercent { get; set; }
        public int RuinOrgasmSometimesPercent { get; set; }
        public int RuinOrgasmRarelyPercent { get; set; }
        public bool IsTimeStampEnabled { get; set; }

        /// <summary>
        /// Name of the domme personality.
        /// </summary>
        public string DommePersonality { get; set; }
    }
}
