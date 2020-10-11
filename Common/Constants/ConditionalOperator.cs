using System;
using System.Collections.Generic;
using System.Text;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Various operators that can be used with conditionals
    /// </summary>
    public static class ConditionalOperator
    {
        /// <summary>
        /// </summary>
        public const string EqualTo = "==";

        /// <summary>
        /// </summary>
        public const string NotEqualTo = "<>";

        /// <summary>
        /// <para>Numeric comparison only</para>
        /// </summary>
        public const string GreaterThan = ">";
        
        /// <summary>
        /// <para>Numeric comparison only</para>
        /// </summary>
        public const string GreaterThanOrEqualTo = ">=";
        
        /// <summary>
        /// <para>Numeric comparison only</para>
        /// </summary>
        public const string LessThan = "<";
        
        /// <summary>
        /// <para>Numeric comparison only</para>
        /// </summary>
        public const string LessThanOrEqualTo = "<=";
    }
}
