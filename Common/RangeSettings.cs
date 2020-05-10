using TeaseAI.Common.Constants;

namespace TeaseAI.Common
{
    public class RangeSettings
    {

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
        /// Does the domme determine how long the tease should be (based on <see cref="DomLevel"/>)
        /// </summary>
        public bool IsTeaseLengthDommeDetermined { get; set; }

        /// <summary>
        /// Does the domme decide the taunt cycle (based on <see cref="DomLevel"/>) 
        /// </summary>
        public bool IsTauntCycleDommeDetermined { get; set; }

        /// <summary>
        /// approximate minimum time for a tease to run
        /// </summary>
        public int TeaseLengthMinutesMinimum { get; set; }

        /// <summary>
        /// approximate maximum time for a tease to run
        /// </summary>
        public int TeaseLengthMinutesMaximum { get; set; }

        /// <summary>
        /// approximate minimum time for a taunt to run
        /// </summary>
        public int TauntCycleMinutesMinimum { get; set; }

        /// <summary>
        /// approximate maximum time for a taunt to run
        /// </summary>
        public int TauntCycleMinutesMaximum { get; set; }
    }
}
