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

        /// <summary>
        /// Used in censorship sucks. Should the censorship bar always be visible
        /// </summary>
        public bool IsContentAlwaysCensored { get; set; }
        
        /// <summary>
        /// Minimum number of seconds the censorship bar should be shown
        /// </summary>
        public int CensorshipBarOnMaximum { get; set; }
        
        /// <summary>
        /// Maximum number of seconds the censorship bar should be shown
        /// </summary>
        public int CensorshipBarOnMinimum { get; set; }
        
        /// <summary>
        /// Minimum number of seconds the censorship bar should be hidden
        /// </summary>
        public int CensorshipBarOffMinimum { get; set; }

        /// <summary>
        /// Maximum number of seconds the censorship bar should be hidden
        /// </summary>
        public int CensorshipBarOffMaximum { get; set; }

        /// <summary>
        /// Percent chance for a given video taunt
        /// </summary>
        public int VideoTauntFrequency { get; set; }

        /// <summary>
        /// Minimum number of seconds the light will be red
        /// </summary>
        public int RedLightMinimumSeconds { get; set; }

        /// <summary>
        /// Maximum number of seconds the light will be red
        /// </summary>
        public int RedLightMaximumSeconds { get; set; }

        /// <summary>
        /// Minimumm number of seconds
        /// </summary>
        public int GreenLightMinimumSeconds { get; set; }

        /// <summary>
        /// Maximum number of seconds the light will be green
        /// </summary>
        public int GreenLightMaximumSeconds { get; set; }
    }
}
